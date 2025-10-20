# Redis Notları

## Caching Nedir?

Yazılım süreçlerinde verilere daha hızlı erişebilmek için bu verilerin bellekte saklanmasına **caching (önbellekleme)** denir.

Bilgisayar sistemlerinde kullanılan bellek türleri arasında hız ve kapasite açısından belirgin farklılıklar bulunur. Örneğin sabit diske kıyasla RAM, verilere anlık erişim açısından çok daha hızlıdır. Bu fark, belirli verilerin RAM’de tutulmasını yazılım açısından avantajlı hale getirir.

Caching, veri erişim hızını artırır ve aynı verilerin tekrar tekrar elde edilmesi gereken durumlarda sunucunun yükünü hafifletir. Çünkü veriler önceden cache’de saklandığı için, ihtiyaç duyulduğunda doğrudan bellekteki veriye ulaşılır. Böylece hem yanıt süresi kısalır hem de sistem kaynakları daha verimli kullanılır.

![image.png](image.png)

### Hangi Tür Veriler Cache’lenir?

Cache’lenecek veriler, **sıklıkla erişilen** ve **hızlı ulaşılması gereken** verilerdir. Bunlara örnek olarak:

- Sık kullanılan veritabanı sorgularının sonuçları
- Konfigürasyon (yapılandırma) verileri
- Menü bilgileri
- Yetkilendirme (authorization) verileri

Ancak her verinin cache’lenmesi uygun değildir. Özellikle **sürekli güncellenen**, **kişisel bilgi içeren** veya **güvenlik açısından risk oluşturabilecek** verilerin cache’lenmesi önerilmez. Çünkü bu tür veriler cache’te güncelliğini yitirirse sistemin yanlış veya eski veriyle çalışmasına yol açabilir.

---

### Cache Mekanizmasının Temel Bileşenleri

- **Cache Belleği:**
    
    Verilerin saklandığı ve hızlı erişim için kullanılan bellek alanıdır.
    
- **Cache Bellek Yönetimi:**
    
    Cache belleğinde saklanan verilerin ne kadar süreyle tutulacağı, ne zaman silineceği ve nasıl güncelleneceği gibi süreçleri yönetir.
    
- **Cache Algoritması:**
    
    Verilerin cache belleğe nasıl ekleneceğini ve ne zaman silineceğini belirleyen algoritmadır. (Örneğin: LRU – *Least Recently Used*, LFU – *Least Frequently Used*, FIFO vb.)
    

> Not: Cache bellek yönetimi yapılırken, verilerin türüne ve kullanım sıklığına göre cache süreleri farklılık gösterebilir. Bu nedenle her veri tipi için uygun bir süre belirlemek performans açısından kritik öneme sahiptir.
> 

---

## Caching Yaklaşımları

Caching uygulamak için iki temel yaklaşım bulunur:

1. **In-Memory Caching:**
    
    Veriler, uygulamanın çalıştığı bilgisayarın belleğinde (RAM) tutulur. Bu yöntem oldukça hızlıdır, ancak veriler yalnızca tek bir sunucuda bulunduğundan ölçeklenebilirlik sınırlıdır.
    
2. **Distributed Caching:**
    
    Veriler birden fazla sunucuda paylaşılır ve bu sayede veriler farklı fiziksel noktalarda bulunur. Bu yöntem, yüksek erişilebilirlik ve hata toleransı sağlar. Büyük ölçekli sistemlerde genellikle Redis veya Memcached gibi teknolojilerle uygulanır.
    

Distributed caching, sistemin farklı bileşenleri arasında **veri bütünlüğü**, **güvenlik** ve **yük dengeleme** açısından daha güvenilir bir yapı sunar.

## Redis Veri Türleri :

| Veri Yapısı | Açıklama |
| --- | --- |
| String | Redis'in en temel, en basit veri türüdür. Metin, sayı veya binary veri saklamak için kullanılır. Hatta binary olarak resim, dosya vs. verileri de saklanabilmektedir. |
| List | Değerleri koleksiyonel olarak tutan bir türdür. |
| Set | Verileri rastgele bir düzende unique bir biçimde tutan veri türüdür. |
| Sorted Set | Set'in düzenli bir şekilde tutulan versiyonudur. |
| Hash | Key-Value formatında veri tutan türdür. |
| Streams | Log gibi hareket eden bir veri türüdür. Streams, event'lerin oluştukları sırayla kaydedilmelerini ve daha sonra işlenmelerini sağlar. |
| Geospatial Indexes | Coğrafi koordinatların saklanmasını sağlayan veri türüdür. |

### Redis Strings :

| KOMUT | İŞLEVİ | ÖRNEK |
| --- | --- | --- |
| SET | Ekleme | SET NAME hilmi |
| GET | Okuma | GET NAME |
| GETRANGE | Karakter Aralığı Okuma | GETRANGE NAME 1 2 |
| INCR & INCRBY | Arttırma | INCR SAYI |
| DECR & DECRBY | Azaltma | DECR SAYI |
| APPEND | Üzerine Ekleme | APPEND NAME celayir |

### Redis Lists

| KOMUT | İŞLEVİ | ÖRNEK |
| --- | --- | --- |
| LPUSH | Başa Veri Ekleme | LPUSH NAMES hilmi ahmet |
| LRANGE | Verileri Listeleme | LRANGE NAMES 0 -1 |
| RPUSH | Sona Veri Ekleme | RPUSH NAMES rifki |
| LPOP | İlk Datayı Çıkarma | LPOP NAMES |
| RPOP | Son Datayı Çıkarma | RPOP NAMES |
| LINDEX | İndexe Göre Datayı Getirme | LINDEX NAMES 1 |
|  |  |  |

### Redis Set

| KOMUT | İŞLEVİ | ÖRNEK |
| --- | --- | --- |
| SADD | Ekleme | SADD COLOR red blue green orange |
| SREM | Silme | SREM COLOR blue |
| SISMEMBER | Arama | SISMEMBER COLOR red |
| SINTER | İki Set'teki Kesişimi Getirir | SINTER user1:BOOKS user2:BOOKS |
| SCARD | Eleman Sayısını Getirir | SCARD COLOR |

### Redis Sorted Set

Birbirinden farklı değerleri sıralı bir şekilde ve unique olarak tutan bir veri türüdür. Her seviye score adı verilen değer atanır ve bu değerler kullanılarak veriler sıralanır.

| KOMUT | İŞLEVİ | ÖRNEK |
| --- | --- | --- |
| ZADD | Ekleme | ZADD TEAMS 1 A |
| ZRANGE | Getirme | ZRANGE TEAMS 0 -1<br>ZRANGE TEAMS 0 -1 WITHSCORES |
| ZREM | Silme | ZREM TEAMS A |
| ZREVRANK | Sıralama Öğrenme | ZREVRANK TEAMS B |

### Redis Hash

Key-Value formatında veri tutan veri türüdür.

| KOMUT | İŞLEVİ | ÖRNEK |
| --- | --- | --- |
| HMSET & HSET | Ekleme | HMSET EMPLOYEES username gncv<br>HSET EMPLOYEES username gncv |
| HMGET & HGET | Getirme | HMGET EMPLOYEES username |
| HDEL | Silme | HDEL EMPLOYEES username |
| HGETALL | Tümünü Getirme | HGETALL EMPLOYEES |

## In-Memory Caching :

Aşağıdaki işlem sırasını takip ederek uygulamalarınızda redis in-momory cache’İ implement edebilirsiniz.

1. **AddMemoryCache** servisini uygulamaya ekleyiniz.
2. **IMemoryCache** referansını inject ediniz.
3. **Set** metoduyla veriyi cache'leyebilir, **Get** metoduyla cache'lenmiş veriyi elde edebilirsiniz.
4. **Remove** fonksiyonuyla cache'lenmiş veriyi silebilirsiniz.
5. **TryGetValue** metodu ile kontrollü bir şekilde cache'den veriyi okuyabilirsiniz.

Absolute Time :  Cachedeki datanın ne kadar tutulacağına dair net ömrünün belirtilmesidir. Belirtilen süre sona erdiğinde cache direkt olarak temizlenir.

Sliding Time : Cachelenmiş datanın memory’de belirtilen süre içerisinde tutulmasını belirtir .Belirtilen süre içerisinde cache’e yapılan erişim neticesinde de datanın ömrü bir o kadar uzatılır.Belirtilen süre zarfında erişim olmazsa cache temizlenir.

![image.png](image%201.png)

## Distrubuted Cache :

Distrubuted cache yaklaşımını uygulamanıza implemente etmek için aşağıdaki yaklaşımı takip edebilirsiniz :

- **StackExchangeRedis** kütüphanesini uygulamaya yükleyiniz.
- **AddStackExchangeRedisCache** servisini uygulamaya ekleyiniz.
- **IDistributedCache** referansını inject ediniz.
- **SetString** metodu ile metinsel, **Set** metodu ile ise binary olarak verilerinizi redis'e cache'leyebilirsiniz. Aynı şekilde **GetString** ve **Get** fonksiyonlarıyla cache'lenmiş verileri elde edebilirsiniz.
- **Remove** fonksiyonu ile cache'lenmiş verileri silebilirsiniz.

![image.png](image%202.png)

## Redis/ Pub-Sub ve Message Broker

Redis ağırlıklı olarak caching yönüyle tercih ediliyor olsa da , bir message broker olarak da kullanılabilmektedir. Rediste bu pub-sub işlemini gerçekleştirmek için birkaç farklı yöntem mevcuttur. Redis CLI, pub/sub işlemlerini yönetmek için kullanışlı bir araçtır. Redis'teki verileri sorgulamak ve pub/sub işlemlerini test etmek amacıyla kullanılabilir. Redis'in pub/sub işlemi gerçekleştirilebilmesi için çeşitli dil ve platformlarda kütüphaneler mevcuttur. Node.js için redis-node, Python için redis-py, .NET Core için StackExchange.Redis gibi kütüphaneler kullanılabilir.

### Redis Pattern-Matching Subscription

Redis, Pattern-Matching Subscription modeli sayesinde abonelerin belirli kalıplara (pattern) ya da desenlerde mesajlar almasını sağlamaktadır. Bu model, abonelerin birden fazla farklı pattern'lara sahip kanallardan mesaj almasını yahut belirli bir kalıba uyan kanalları filtrelemesini mümkün kılar. Misal olarak, bir abonenin sadece 'stock.*' pattern'ına uygun olan kanallardan almasını isterseniz; 'stock.apple', 'stock.google' ve 'stock.amazon' gibi kanallardan mesajları alabilir ancak 'news.tech' isimli kanaldan ise doğal olarak mesajları filtrelemiş olursunuz

## Redis Replication
