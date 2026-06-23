using System;

namespace MemoryForge
{
    class Car
    {
        public string Model;
        public string Color;
        public Car(string model, string renk)
        {
            this.Model = model;
            this.Color = renk;
        }
    }
    public class GCSentinel
    {
        public void ReportMemory()
        {
            long totalMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"[MemoryWatch] Current RAM Usage: {totalMemory / 1024.0 / 1024.0:F2} MB");
        }

        public void CheckGenerations(object obj)
        {
            int gen = GC.GetGeneration(obj);
            Console.WriteLine($"[GenTracker] This object currently resides in Gen {gen}.");
        }
    }
    
    class Program
    {
        static void Main(string[] args) 
        {
            GCSentinel sentinel = new GCSentinel();
            List<Car> gallery = new List<Car>();

            Console.WriteLine("--- Stress Test Çalıştırılıyor ---");
            sentinel.ReportMemory();
            for (int i = 0; i < 100000; i++)
            {
                gallery.Add(new Car("Red", $"Model {i}"));
                
                if (i % 10000 == 0)
                {
                    sentinel.ReportMemory();
                }
            }

            Console.WriteLine("\n Object Creating");
            sentinel.ReportMemory();
            sentinel.CheckGenerations(gallery[0]);
            
            Console.WriteLine("\n Liste temizleniyor");
            gallery.Clear(); 
            gallery = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            sentinel.ReportMemory();
            Console.WriteLine("Test bitti");
            
            Console.ReadLine();
        }
    }
}