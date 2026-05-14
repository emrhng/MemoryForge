using System;

namespace MemoryForge
{
    class Araba
    {
        public string Model;
        public string Renk;
        public Araba(string model, string renk)
        {
            this.Model = model;
            this.Renk = renk;
        }
    }
    public class GCSentinel
    {
        public void ReportMemory()
        {
            long totalMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"[MemoryWatch] Şu anki RAM Kullanımı: {totalMemory / 1024.0 / 1024.0:F2} MB");
        }

        public void CheckGenerations(object obj)
        {
            int gen = GC.GetGeneration(obj);
            Console.WriteLine($"[GenTracker] Bu nesne şu an Gen {gen} içerisinde yaşıyor.");
        }
    }
    
    class Program
    {
        static void Main(string[] args) 
        {
            GCSentinel sentinel = new GCSentinel();
            List<Araba> galeri = new List<Araba>();

            Console.WriteLine("--- Stres Testi Başlıyor ---");
            sentinel.ReportMemory();
            for (int i = 0; i < 100000; i++)
            {
                galeri.Add(new Araba("Kırmızı", $"Model {i}"));
                
                if (i % 10000 == 0)
                {
                    sentinel.ReportMemory();
                }
            }

            Console.WriteLine("\nNesneler Oluşturuldu");
            sentinel.ReportMemory();
            sentinel.CheckGenerations(galeri[0]);
            
            Console.WriteLine("\nListe Boşaltılıyor ve Temizlik Başlıyor");
            galeri.Clear(); 
            galeri = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            sentinel.ReportMemory();
            Console.WriteLine("Test Bitti");
            
            Console.ReadLine();
        }
    }
}