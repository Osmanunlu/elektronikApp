using System;
using System.Collections.Generic;
using System.Linq;

namespace ElektronikLibrary
{
    public interface IUrun
    {
        int ID { get; }
        string UrunBilgisi();
    }

    public abstract class Urun : IUrun
    {
        private string ad;
        private decimal fiyat;
        private int stok;
        private int id;

        public string Ad
        {
            get => ad;
            set => ad = string.IsNullOrWhiteSpace(value) ? "Bilinmiyor" : value;
        }

        public decimal Fiyat
        {
            get => fiyat;
            set => fiyat = value < 0 ? 0 : value;
        }

        public int Stok
        {
            get => stok;
            set => stok = value < 0 ? 0 : value;
        }

        public int ID
        {
            get => id;
            set => id = value <= 0 ? new Random().Next(100000, 999999) : value;
        }

        public Urun(string ad, decimal fiyat, int stok, int id)
        {
            Ad = ad;
            Fiyat = fiyat;
            Stok = stok;
            ID = id;
        }

        public abstract string UrunBilgisi();
    }

    public class Telefon : Urun
    {
        public Telefon(string ad, decimal fiyat, int stok, int id) : base(ad, fiyat, stok, id) { }

        public override string UrunBilgisi()
        {
            return $"[Telefon] ID: {ID}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class Bilgisayar : Urun
    {
        public Bilgisayar(string ad, decimal fiyat, int stok, int id) : base(ad, fiyat, stok, id) { }

        public override string UrunBilgisi()
        {
            return $"[Bilgisayar] ID: {ID}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class Aksesuar : Urun
    {
        public Aksesuar(string ad, decimal fiyat, int stok, int id) : base(ad, fiyat, stok, id) { }

        public override string UrunBilgisi()
        {
            return $"[Aksesuar] ID: {ID}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class ElektronikMarket
    {
        private List<IUrun> urunler;

        public ElektronikMarket()
        {
            urunler = new List<IUrun>();
        }

        public void UrunEkle(IUrun urun)
        {
            urunler.Add(urun);
        }

        public List<IUrun> UrunleriGetir()
        {
            return urunler;
        }

        public bool UrunSat(int id, int miktar)
        {
            var urun = urunler.FirstOrDefault(u => u.ID == id);
            if (urun is Urun urunDetay && urunDetay.Stok >= miktar)
            {
                urunDetay.Stok -= miktar;
                return true;
            }
            return false;
        }

        public void SaveToCsv(string filePath)
        {
            var lines = new List<string> { "Tür,Ad,Fiyat,Stok,ID" };
            foreach (var urun in urunler)
            {
                if (urun is Telefon telefon)
                {
                    lines.Add($"Telefon,{telefon.Ad},{telefon.Fiyat},{telefon.Stok},{telefon.ID}");
                }
                else if (urun is Bilgisayar bilgisayar)
                {
                    lines.Add($"Bilgisayar,{bilgisayar.Ad},{bilgisayar.Fiyat},{bilgisayar.Stok},{bilgisayar.ID}");
                }
                else if (urun is Aksesuar aksesuar)
                {
                    lines.Add($"Aksesuar,{aksesuar.Ad},{aksesuar.Fiyat},{aksesuar.Stok},{aksesuar.ID}");
                }
            }
            System.IO.File.WriteAllLines(filePath, lines);
        }

        public void LoadFromCsv(string filePath)
        {
            if (!System.IO.File.Exists(filePath)) return;

            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // İlk satır başlık
            {
                var parts = line.Split(',');
                var tur = parts[0];
                var ad = parts[1];
                var fiyat = decimal.Parse(parts[2]);
                var stok = int.Parse(parts[3]);
                var id = int.Parse(parts[4]);

                if (tur == "Telefon")
                {
                    UrunEkle(new Telefon(ad, fiyat, stok, id));
                }
                else if (tur == "Bilgisayar")
                {
                    UrunEkle(new Bilgisayar(ad, fiyat, stok, id));
                }
                else if (tur == "Aksesuar")
                {
                    UrunEkle(new Aksesuar(ad, fiyat, stok, id));
                }
            }
        }
    }
}