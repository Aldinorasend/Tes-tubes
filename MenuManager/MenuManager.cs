using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace MenuManager
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public MenuItem() { }
        public MenuItem(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    public class MenuManager<T> where T : MenuItem
    {
        private List<T> items = new List<T>();
        private int nextId = 1;

        public void AddItem(int id, string name, int price)
        {
            Contract.Requires(id >= nextId, "ID Harus lebih dari 0");
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "Nama menu tidak kosong");
            Contract.Requires(price >= 0, "Harga tidak boleh dari .");
            Contract.Requires(!items.Exists(i => i.Id == id), "ID harus unique.");
            Contract.Ensures(items.Exists(i => i.Id == id), "Item sudah ada di list");

            var item = (T)Activator.CreateInstance(typeof(T));
            item.Id = id;
            item.Name = name;
            item.Price = price;
            items.Add(item);

            if (id >= nextId)
            {
                nextId = id + 1;
            }
        }

        public void UpdateItem(int id, string name, int price)
        {
            Contract.Requires(id > 0, "ID Harus lebih dari 0\"");
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "Nama menu tidak kosong");

            var item = items.Find(i => i.Id == id);
            if (item != null)
            {
                item.Name = name;
                item.Price = price;
                Contract.Ensures(item.Name == name, "Name Menu harus di update");
                Contract.Ensures(item.Price == price, "Harga harus di update");
            }
            else
            {
                Console.WriteLine($"Item dengan ID {id} tidak ditemukan.");
            }
        }

        public List<T> GetItems()
        {
            Contract.Ensures(Contract.Result<List<T>>() != null, "Items tidak bisa null");
            return items;
        }

        public void RemoveItem(int id)
        {
            Contract.Requires(id > 0, "ID Harus lebih dari 0");

            var item = items.Find(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
                Contract.Ensures(!items.Contains(item), "Item harus dihilangkan dari list");
            }
            else
            {
                Console.WriteLine($"Item dengan ID {id} tidak ditemukan.");
            }
        }

        public void DisplayMenu()
        {
            Contract.Assert(items != null, "Items tidak boleh null");

            Console.WriteLine("Daftar Menu:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Nama: {item.Name}, Harga: {item.Price:C}");
            }
        }
    }
}
