using OrderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary
{
    public class OrderIn
    {

        public int Id { get; set; }
        public List<OrderLibrary.MenuItem> Menu { get; set; }
        public string Description { get; set; }
        public OrderStatus Status => StateMachine.CurrentState;



        private OrderStateMachine StateMachine { get; set; }

        public OrderIn(int idOrder, List<MenuItem> menu, string description)
        {
            Id = idOrder;
            Menu = menu;
            Description = description;
            StateMachine = new OrderStateMachine();
        }

        public bool ApplyEvent(OrderEvent orderEvent)
        {
            return StateMachine.ApplyEvent(orderEvent);
        }
        public int GetTotal()
        {
            return Menu.Sum(item => item.Price);
        }
    }
}
