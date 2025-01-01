using System;
using ElektronikLibrary;

namespace ElektronikConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ElektronikMarket market = new ElektronikMarket();
            market.LoadFromCsv("../../../urunler.csv");

            Console.WriteLine("Elektronik Satış Uygulamasına Hoş Geldiniz!");
            while (true)
            {
                Console.WriteLine("\nMenü:");
                Console.WriteLine("1. Ürün Ekle");
                Console.WriteLine("2. Ürünleri Görüntüle");
                Console.WriteLine("3. Ürün Sat");
                Console.WriteLine("4. Ürün Güncelle");
                Console.WriteLine("5. Çıkış");

                Console.Write("Bir seçenek seçin: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        UrunEkle(market);
                        break;

                    case "2":
                        UrunleriGoruntule(market);
                        break;

                    case "3":
                        UrunSat(market);
                        break;

                    case "4":
                        UrunGuncelle(market);
                        break;

                    case "5":
                        market.SaveToCsv("../../../urunler.csv");
                        Console.WriteLine("Uygulamadan çıkılıyor. Hoşça kalın!");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçenek. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void UrunEkle(ElektronikMarket market)
        {
            Console.WriteLine("\nÜrün Türünü Seçin:");
            Console.WriteLine("1. Bilgisayar");
            Console.WriteLine("2. Telefon");
            Console.WriteLine("3. Aksesuar");

            string turSecim;
            while (true)
            {
                turSecim = Console.ReadLine();
                if (turSecim == "1" || turSecim == "2" || turSecim == "3")
                {
                    break;
                }
                Console.WriteLine("Geçersiz ürün türü seçimi. Lütfen tekrar deneyin.");
            }

            Console.Write("Ürün adını girin: ");
            string ad = Console.ReadLine();

            decimal fiyat;
            while (true)
            {
                Console.Write("Ürün fiyatını girin: ");
                if (decimal.TryParse(Console.ReadLine(), out fiyat) && fiyat >= 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz fiyat girdiniz. Lütfen tekrar deneyin.");
            }

            int stok;
            while (true)
            {
                Console.Write("Ürün stok miktarını girin: ");
                if (int.TryParse(Console.ReadLine(), out stok) && stok >= 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz stok miktarı girdiniz. Lütfen tekrar deneyin.");
            }

            int id;
            while (true)
            {
                Console.Write("Ürün ID numarasını girin: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz ID numarası girdiniz. Lütfen tekrar deneyin.");
            }

            switch (turSecim)
            {
                case "1":
                    market.UrunEkle(new Bilgisayar(ad, fiyat, stok, id));
                    break;
                case "2":
                    market.UrunEkle(new Telefon(ad, fiyat, stok, id));
                    break;
                case "3":
                    market.UrunEkle(new Aksesuar(ad, fiyat, stok, id));
                    break;
            }

            market.SaveToCsv("../../../urunler.csv");
            Console.WriteLine("Ürün başarıyla eklendi.");
        }

        static void UrunleriGoruntule(ElektronikMarket market)
        {
            Console.WriteLine("\nMevcut Ürünler:");
            foreach (var urun in market.UrunleriGetir())
            {
                Console.WriteLine(urun.UrunBilgisi());
            }
        }

        static void UrunSat(ElektronikMarket market)
        {
            Console.WriteLine("\nMevcut Ürünler:");
            UrunleriGoruntule(market);

            int id;
            while (true)
            {
                Console.Write("\nSatılacak ürünün ID numarasını girin: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (market.UrunleriGetir().Exists(u => u.ID == id))
                    {
                        break;
                    }
                    Console.WriteLine("Girilen ID numarası listede bulunamadı. Lütfen tekrar deneyin.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ID girdiniz. Lütfen tekrar deneyin.");
                }
            }

            while (true)
            {
                Console.Write("Satılacak miktarı girin: ");
                if (int.TryParse(Console.ReadLine(), out int miktar) && miktar > 0)
                {
                    var urun = market.UrunleriGetir().Find(u => u.ID == id) as Urun;
                    if (urun != null && urun.Stok >= miktar)
                    {
                        if (market.UrunSat(id, miktar))
                        {
                            market.SaveToCsv("../../../urunler.csv");
                            Console.WriteLine("Ürün başarıyla satıldı.");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Yetersiz stok. Mevcut stok: {urun?.Stok ?? 0}. Lütfen tekrar deneyin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz miktar girdiniz. Lütfen tekrar deneyin.");
                }
            }
        }

        static void UrunGuncelle(ElektronikMarket market)
        {
            UrunleriGoruntule(market);

            Console.Write("\nGüncellenecek ürünün ID numarasını girin (Çıkmak için '0' girin): ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        Console.WriteLine("Güncelleme işlemi iptal edildi.");
                        return;
                    }

                    if (market.UrunleriGetir().Exists(u => u.ID == id))
                    {
                        var urun = market.UrunleriGetir().Find(u => u.ID == id) as Urun;

                        Console.Write("Yeni ürün adını girin: ");
                        while (true)
                        {
                            string yeniAd = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(yeniAd))
                            {
                                urun.Ad = yeniAd;
                                break;
                            }
                            Console.WriteLine("Geçersiz ad. Lütfen tekrar deneyin.");
                        }

                        Console.Write("Yeni ürün fiyatını girin: ");
                        while (true)
                        {
                            if (decimal.TryParse(Console.ReadLine(), out decimal yeniFiyat) && yeniFiyat >= 0)
                            {
                                urun.Fiyat = yeniFiyat;
                                break;
                            }
                            Console.WriteLine("Geçersiz fiyat. Lütfen tekrar deneyin.");
                        }

                        Console.Write("Yeni stok miktarını girin: ");
                        while (true)
                        {
                            if (int.TryParse(Console.ReadLine(), out int yeniStok) && yeniStok >= 0)
                            {
                                urun.Stok = yeniStok;
                                break;
                            }
                            Console.WriteLine("Geçersiz stok miktarı. Lütfen tekrar deneyin.");
                        }

                        market.SaveToCsv("../../../urunler.csv");
                        Console.WriteLine("Ürün başarıyla güncellendi.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Girilen ID numarası bulunamadı. Lütfen tekrar deneyin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz ID girdiniz. Lütfen tekrar deneyin.");
                }
            }
        }
    }
}