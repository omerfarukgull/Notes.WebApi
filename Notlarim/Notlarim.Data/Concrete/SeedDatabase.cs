using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Concrete
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            //var context = new NoteContext();

            //if (context.Database.GetPendingMigrations().Count() == 0)// bekleyen migration varmı diye bakıyor
            //{
            //    if (context.Categories.Count() == 0)
            //    {
            //        context.Categories.AddRange(Categories);
            //    }
            //    if (context.Members.Count() == 0)
            //    {
            //        context.Members.AddRange(Members);

            //    }
                
                
            //    if (context.Notes.Count() == 0)
            //    {
            //        context.Notes.AddRange(Notes);

            //    }
                  
            //    if (context.Messages.Count() == 0)
            //    {
            //        context.Messages.AddRange(Messages);

            //    }
              
            //}
            //context.SaveChanges();
        }
        //private static Category[] Categories = {
        //    new Category(){CategoryName="Yazılım Mühendisliği"},
        //    new Category(){CategoryName="Elektrik ve Elektronik Mühendisliği"},
        //    new Category(){CategoryName="Endüstri Mühendisliği"},
        //    new Category(){CategoryName="Makine Mühendisliği"},
        //    new Category(){CategoryName="Bilgisayar Mühendisliği"},
        //};
        //private static Member[] Members = {
        //    new Member(){Name="Ömer Faruk",SurName="Gül",Email="omer@gmail.com",Password="123",PhoneNumber="5355050715",Gender="Erkek",MemberImageUrl="erkek-user.jpeg",CreatDate=new DateTime(2023,07,18),UserStatu="User" ,University="Sakarya Üniversitesi",Department="Bilgisayar Programcılığı"},
        //    new Member(){Name="Yesim",SurName="Kaplan",Email="yesim@gmail.com",Password="123",PhoneNumber="5457894521",Gender="Kadın",MemberImageUrl="bayan-user.png",CreatDate=new DateTime(2023,07,18),UserStatu="User"  ,University="Marmara Üniversitesi",Department="Bilgisayar Mühensiliği"},
        //    new Member(){Name="Admin",SurName="Admin",Email="admin@gmail.com",Password="123",PhoneNumber="5474511457",Gender="Erkek",MemberImageUrl="erkek-user.jpeg",CreatDate=new DateTime(2023,07,18),UserStatu="Admin"  ,University="Boğaziçi Üniversitesi",Department="Yazılım Mühensiliği"},
        //};
      
        
        //private static Note[] Notes = {
        //    new Note(){Title="Nesne Yönelimli Programlama (OPP)",Content="Öncelikle OOP kavramına bakalım. OOP (Object-Oriented Programming) ders içeriği veya kaynakları açısından Nesne Yönelimli Programlama (NYP) olarak adlandırılabilir. Dilin temeli, C# OOP programlarında ortaya çıkan kaotik ortamı ortadan kaldırmak için 1960’ların sonlarında ortaya çıkan OOP konseptine dayanılarak atıldı. Programlarımızda OOP kullanarak, daha güvenli olma, daha kolay kontrol etme ve yanlış sonuçları anında tespit etme, böylece daha kolay kod blokları yazma fırsatına sahip oluruz.",NoteImgUrl="opp1.png",NotePdfUrl="Nesneye-Dayali-Programlama-Giris.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=1,MemberId=1,IsApproved=true},
        //    new Note(){Title="OPP Sınıflar",Content="Birbirine bağlı, tamamen bir bütünü temsil eden, nesneye yönelik programlamanın ilk adımıdır.",NoteImgUrl="siniflar.jpg",NotePdfUrl="Siniflar.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=1,MemberId=1,IsApproved=true},
        //    new Note(){Title="OPP Polymorphism",Content="Programımızda oluşan bir nesne yapısının birbirinden farklı nesneler şeklinde davranmasını sağlayan yapı",NoteImgUrl="polymorphism.png",NotePdfUrl="Polymorphism.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=5,MemberId=2,IsApproved=true},
        //    new Note(){Title="Analog Elektronik ",Content="Analog elektronik sistem, sayısalın aksine sürekli sinyal kullanan elektronik sistemdir. Analog elektronik sistemi gösteren bir diyot devresi",NoteImgUrl="analog-elektronik.jpg",NotePdfUrl="analog-elektronik.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=2,MemberId=2,IsApproved=true},
        //    new Note(){Title="Kablosuz İletisim",Content="Kablosuz iletişim, kablolu iletişimin yanı sıra bir noktadan başka bir noktaya kablo hattı kullanmadan veri, ses veya görüntü taşınmasına denir.",NoteImgUrl="Elek-Kablosuz-iletisim.jpg",NotePdfUrl="Elek-Kablosuz-iletisim.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=2,MemberId=2,IsApproved=true},
        //    new Note(){Title="Elektronik Devreler",Content="Elektronik devreler; direnç, diyot, transistör, pil yatağı, kondansatör ve indüktör gibi devre elemanlarının birbirlerine bağlanmasıyla oluşturulan düzeneklerdir. ",NoteImgUrl="elektronik-devreler.jpg",NotePdfUrl="elektronik-devreler.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=2,MemberId=1,IsApproved=false},
        //    new Note(){Title="Makine Mühendisligi Konuları",Content="Makine Mühendisligi Konuları Bulunmaktadır ",NoteImgUrl="Makine-Mühendisligi.jpg",NotePdfUrl="MAK110_Makine_muhendisligi_konulari.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=4,MemberId=1,IsApproved=false},
        //    new Note(){Title="Makine Elemanları",Content="Dışarıdan bakıldığında çok karışık görünen bir makine bile aslında temel bazı elemanların bir araya getirilerek birbirine monte edilmesi ile oluşur. İşte birbirine montajlanarak makineyi oluşturan bu parçaların mukavemetini, konstrüksiyon özelliklerini göz önüne alarak tasarımı gerçekleştiren bilim dalına makine elemanları denir. ",NoteImgUrl="Makine-elemanları.jpg",NotePdfUrl="Makine-elemanları.pdf",CreatDate=new DateTime(2023,07,18),CategoryId=4,MemberId=2,IsApproved=false},

        //};
        
        //private static Messages[] Messages = {
        //    new Messages(){Name="Can Konak",Email="can@gmail.com",Title="Not Paylaşımı",Message="Not Palaşımını nasıl yapabilirim?",CreatDate=new DateTime(2023,07,18), MessageStatus=true  },
        //    new Messages(){Name="Hamdi Genel",Email="hamdi@gmail.com",Title="Kategori Ekle",Message="Grafik Tasarım Kategori Eklermisiz?",CreatDate=new DateTime(2023,07,18), MessageStatus=false  },
        //    new Messages(){Name="Barış Bayrak",Email="baris@gmail.com",Title="Not İndir",Message="Not indirmek için üye olmak mı lazım?",CreatDate=new DateTime(2023,07,18), MessageStatus=false  },
        //};

    }
}
