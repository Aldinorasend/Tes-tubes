using OrderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
    public enum OrderEvent
    {
        FinishProcessing,
        CancelOrder
    }
    public class OrderStateMachine
    {
        private Dictionary<(OrderStatus, OrderEvent), OrderStatus> _transitions;
        public OrderStatus CurrentState { get; private set; }

        public OrderStateMachine()
        {
            CurrentState = OrderStatus.Pending;

            _transitions = new Dictionary<(OrderStatus, OrderEvent), OrderStatus>
            {
                { (OrderStatus.Pending, OrderEvent.FinishProcessing), OrderStatus.Processing },
                { (OrderStatus.Processing, OrderEvent.FinishProcessing), OrderStatus.Completed },
                { (OrderStatus.Pending, OrderEvent.CancelOrder), OrderStatus.Cancelled },
                { (OrderStatus.Processing, OrderEvent.CancelOrder), OrderStatus.Cancelled }
                // Completed and Cancelled states are terminal; no transitions from here
            };
        }

        public bool ApplyEvent(OrderEvent orderEvent)
        {
            var key = (CurrentState, orderEvent);
            if (_transitions.TryGetValue(key, out var newState))
            {
                CurrentState = newState;
                return true;
            }
            return false;
        }
    }
}
