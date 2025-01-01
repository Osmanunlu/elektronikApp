# Elektronik Market Uygulaması Proje Dokümanı

## Proje Özeti
Bu proje, bir elektronik markette ürünlerin stok takibi ve satış işlemlerini gerçekleştiren bir konsol uygulamasıdır. Proje, kullanıcıların ürün eklemesi, mevcut ürünlerin bilgilerini görmesi, ürün satışı yapması ve ürün bilgilerini dosyaya kaydedip yüklemesi gibi işlemleri yapmasını sağlar.

## Projenin Amacı
Projenin amacı, bir marketin ürün yönetimini ve stok kontrolünü kolaylaştırmak ve bu işlemleri bir yazılım üzerinden yönetmektir.

## Temel Özellikler
1. **Ürün Ekleme:** Market envanterine telefon, bilgisayar ve aksesuar gibi ürünler eklenebilir.
2. **Ürün Listeleme:** Mevcut ürünlerin bilgileri listelenebilir.
3. **Ürün Satışı:** Stokta bulunan ürünlerin satışı yapılabilir.
4. **CSV Dosya İşlemleri:** Ürün bilgileri bir dosyaya kaydedilip sonradan tekrar yüklenebilir.

## Teknik Detaylar
- **Dil:** C#
- **Sınıflar:** Telefon, Bilgisayar ve Aksesuar sınıfları ürünü temsil eder. ElektronikMarket sınıfı tüm işlemleri yönetir.
- **Arayüz:** IUrun adında bir arayüz ile ürünlerin temel özellikleri tanımlanmıştır.

## Örnek Kullanım
- Telefon eklemek: Kullanıcı bir telefonun adını, fiyatını, stok miktarını ve ID numarasını girerek markete yeni bir ürün ekleyebilir.
- Ürün satışı yapmak: Kullanıcı, bir ürünün ID numarasını ve satılacak miktarı girerek satış işlemi yapabilir.
- Dosya işlemleri: Ürün bilgileri bir CSV dosyasına kaydedilebilir ve daha sonra tekrar yüklenebilir.

Bu proje, yazılım geliştirme pratiği yapmayı ve temel C# becerilerini geliştirmeyi amaçlamaktadır.

