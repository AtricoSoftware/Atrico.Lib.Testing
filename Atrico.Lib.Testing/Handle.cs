using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atrico.Lib.Testing
{
    public static class Handle
    {
        private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(1);

        public static EventArgs Event(Action raisingAction, Action<EventHandler> register, Action<EventHandler> deregister)
        {
            return Event(raisingAction, register, deregister, _defaultTimeout);
        }

        public static EventArgs Event(Action raisingAction, Action<EventHandler> register, Action<EventHandler> deregister, TimeSpan timeout)
        {
            return EventImpl<EventArgs, EventHandler>(raisingAction, register, deregister, action => new EventHandler(action), timeout);
        }

        public static TEventArgs Event<TEventArgs>(Action raisingAction, Action<EventHandler<TEventArgs>> register, Action<EventHandler<TEventArgs>> deregister) where TEventArgs : EventArgs
        {
            return Event(raisingAction, register, deregister, _defaultTimeout);
        }

        public static TEventArgs Event<TEventArgs>(Action raisingAction, Action<EventHandler<TEventArgs>> register, Action<EventHandler<TEventArgs>> deregister, TimeSpan timeout) where TEventArgs : EventArgs
        {
            return EventImpl<TEventArgs, EventHandler<TEventArgs>>(raisingAction, register, deregister, action => new EventHandler<TEventArgs>(action), timeout);
        }

        private static TEventArgs EventImpl<TEventArgs, TEventHandler>(Action raisingAction, Action<TEventHandler> register, Action<TEventHandler> deregister, Func<Action<object, TEventArgs>, TEventHandler> handlerFactory, TimeSpan timeout)
            where TEventArgs : class
        {
            var eventSignal = new EventWaitHandle(false, EventResetMode.ManualReset);
            TEventArgs eventArgs = null;
            var handler = handlerFactory((snd, args) =>
            {
                eventArgs = args;
                eventSignal.Set();
            });
            // Register for event
            register(handler);
            // Perform 
            raisingAction();
            // Wait for event
            eventSignal.WaitOne(timeout);
            // Deregister for event
            deregister(handler);
            return eventArgs;
        }
    }
}
