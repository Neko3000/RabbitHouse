namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using RabbitHouse.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RabbitHouse.Models.RabbitHouseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\RabbitHouseDbContext";
        }
        public void SeedDebug(RabbitHouseDbContext context)
        {
            Seed(context);
        }
        protected override void Seed(RabbitHouse.Models.RabbitHouseDbContext context)
        {
            var productProperties = new List<ProductProperty>
            {
                new ProductProperty
                {
                    Name="����",
                    Description="������Բ�α����Ĳ�Ʒ",
                    ImgUrl="/ImgRepository/ProductPropertyImgs/ice.png"
                },
                new ProductProperty
                {
                    Name="����",
                    Description="�������ܱ��µĲ�Ʒ",
                    ImgUrl="/ImgRepository/ProductPropertyImgs/hot.png"
                },
                new ProductProperty
                {
                    Name="���ӽ�",
                    Description="��������ӹ����Ĳ�Ʒ",
                    PlusPrice=1,
                    ImgUrl="/ImgRepository/ProductPropertyImgs/orange.png"
                },
                new ProductProperty
                {
                    Name="��ݮ��",
                    Description="�������ݮ�����Ĳ�Ʒ",
                    PlusPrice=(decimal)1.5,
                    ImgUrl="/ImgRepository/ProductPropertyImgs/orange.png"
                },
            };
            productProperties.ForEach(singleProperty => context.ProductProperties.AddOrUpdate(p => p.Name, singleProperty));
            context.SaveChanges();

            var productCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Name="����",
                    Description="��׼��������"
                },
                new ProductCategory
                {
                    Name="���",
                    Description="����ͬ����һ������һЩ��"
                },
                new ProductCategory
                {
                    Name="��Ʒ",
                    Description="���ǿ��ȵ����϶�������"
                },
                new ProductCategory
                {
                    Name="��ʳ",
                    Description="�ܳԱ��Ķ�����"
                },
            };
            productCategories.ForEach(singleCategory => context.ProductCategories.AddOrUpdate(p => p.Name, singleCategory));
            context.SaveChanges();

            context.Products.AddOrUpdate
            (
                p=>p.Name,
                new Product
                {
                    Name = "����Ħ������",
                    ShortDescription = "��νCOFFEEMOCHA�������Ƕ��ڰ칫���ϵ�����ʱ��",
                    Description = "������Ħ��������һ��~�����������Ǿ�֪�����ǵ�������ʱ���ˡ�Ħ�����ȣ�CafeMocha����һ������ϵĿ��ȣ�����ʷҪ׷�ݵ����ȵ���Դ�������������Ũ�����ȡ��ɿ������������ͺ�ţ�̻�϶��ɣ�Ħ��������������Ħ���ۡ�ʮ�����ͣ������ж��ǿ��ȹ�����������ҵ����ʢ��Ҳ��Ħ���ǵ�ʱ�캣������Ҫ���һ���̸ۣ���ʱ������Ҫ�Ǽ��е�Ħ��������������ķ��޿��ȣ�����ͳ��Ħ�����ȡ������˵ĸۿ���Ȼ������Ħ���۵ĵ�λ������Ħ����ʱ��Ħ�����ȵĲ�����Ȼ��������������Щ���������Ŀ��ȶ����Ա���ΪĦ�����ȶ���",
                    Remark = "����Ħ��������������8���Ż��У������Լ���ʳ�������ܴ˴��Ż� ������Ʒ����ʱ�ṩֽ����װ",
                    Price = (decimal)12.0,
                    CurrentDiscount = (decimal)0.8,
                    DiscountStartTime = new DateTime(2016, 3, 1, 12, 30, 0),
                    DiscountEndTime = new DateTime(2016, 5, 1, 12, 30, 0),
                    PublishTime = new DateTime(2016, 2, 1),
                    IsSeasonalProduct = false,
                    CategoryId=context.ProductCategories.Single(c=>c.Name== "����").Id,
                    Category=context.ProductCategories.Single(c=>c.Name=="����"),
                    CoverImgUrl= "/ImgRepository/ProductImgs/1/dark-chocolate-mocha-avocado-mousse-4.jpg",
                    Properties = new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="����"),
                        context.ProductProperties.Single(p=>p.Name=="����"),
                    }
                },
                new Product
                {
                    Name = "�������������𵰸�",
                    ShortDescription = "��������������ײ��һ�𣬻���������һ���𻨣���",
                    Description = "��Ϊ��������е�������ƷNO.1�����Բ�������ʧ��",
                    Remark = "��������ݮ����ѡ��",
                    Price = (decimal)10.0,
                    PublishTime = new DateTime(2016, 2, 5),
                    IsSeasonalProduct = false,
                    CategoryId = context.ProductCategories.Single(c => c.Name == "���").Id,
                    Category = context.ProductCategories.Single(c => c.Name == "���"),
                    CoverImgUrl = "/ImgRepository/ProductImgs/2/wallhaven-284723.jpg",
                    Properties =new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="���ӽ�"),
                        context.ProductProperties.Single(p=>p.Name=="��ݮ��"),
                    }
                },
                new Product
                {
                    Name = "���ǲ�",
                    ShortDescription = "�������Ƿ��������ں�",
                    Description = "���ǲ���һ���ŵ㡪�������Ǻ���",
                    Remark = "Ŀǰ���д�װ",
                    Price = (decimal)5.0,
                    PublishTime = new DateTime(2016, 5, 1),
                    IsSeasonalProduct = true,
                    SaleStartTime = new DateTime(2016, 6, 10, 0 ,0 ,0),
                    SaleEndTime = new DateTime(2016, 9, 10, 0, 0, 0),
                    CategoryId = context.ProductCategories.Single(c => c.Name == "��Ʒ").Id,
                    Category = context.ProductCategories.Single(c => c.Name == "��Ʒ"),
                    CoverImgUrl = "/ImgRepository/ProductImgs/3/tea-01.jpg",
                    Properties = new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="����"),
                    }
                }
            );
            context.SaveChanges();

            context.ProductImages.AddOrUpdate(
                pI=>pI.Name,
                new ProductImage
                {
                    Name="p1-d1.jpg",
                    Url= "/ImgRepository/ProductImgs/1/p1-d1.jpg",
                    Product= context.Products.Single(c => c.Name == "����Ħ������"),
                    ProductId= context.Products.Single(c => c.Name == "����Ħ������").Id,
                    RecordTime=DateTime.Now
                },
                new ProductImage
                {
                    Name = "p1-d2.jpg",
                    Url = "/ImgRepository/ProductImgs/1/p1-d2.jpg",
                    Product = context.Products.Single(c => c.Name == "����Ħ������"),
                    ProductId = context.Products.Single(c => c.Name == "����Ħ������").Id,
                    RecordTime = DateTime.Now
                },
                new ProductImage
                {
                    Name = "cheese1.jpg",
                    Url = "/ImgRepository/ProductImgs/2/cheese1.jpg",
                    Product = context.Products.Single(c => c.Name == "�������������𵰸�"),
                    ProductId = context.Products.Single(c => c.Name == "�������������𵰸�").Id,
                    RecordTime = DateTime.Now
                },
                new ProductImage
                {
                    Name = "tea1.jpg",
                    Url = "/ImgRepository/ProductImgs/3/tea1.jpg",
                    Product = context.Products.Single(c => c.Name == "���ǲ�"),
                    ProductId = context.Products.Single(c => c.Name == "���ǲ�").Id,
                    RecordTime = DateTime.Now
                }
            );
            context.SaveChanges();

            //article
            var articleCategories = new List<ArticleCategory>
            {
                new ArticleCategory
                {
                    Name="���Ϣ",
                    Description="�����и��ֻ�Ĺ���",
                },
                new ArticleCategory
                {
                    Name="������Ϣ",
                    Description="����ʱ�ؼ���Ʒ���ݵ�֪ͨ",
                },
            };
            articleCategories.ForEach(singleCategory => context.ArticleCategories.AddOrUpdate(p => p.Name, singleCategory));
            context.SaveChanges();

            var articleTags = new List<ArticleTag>
            {
                new ArticleTag
                {
                    Name="����",
                    Description="�����������йص�����",
                },
                new ArticleTag
                {
                    Name="����",
                    Description="�뿧���й�ϵ������",
                },
                new ArticleTag
                {
                    Name="ҹ��",
                    Description="�ص���ҹ�����Ϣ",
                },
            };
            articleTags.ForEach(singleTag => context.ArticleTags.AddOrUpdate(p => p.Name, singleTag));
            context.SaveChanges();

            //articleDialog
            var characters = new List<Character>
            {
                new Character
                {
                    Name="Rise",
                    Description="����һ�����°�����",
                    ImgUrl="/ImgRepository/CharacterImgs/001.jpg",
                    Color="#c7b6ff"
                },
                new Character
                {
                    Name="Chiya",
                    Description="ÿ�춼Ϊ����µ������˵���Ŭ����",
                    ImgUrl="/ImgRepository/CharacterImgs/002.jpg",
                    Color="#b9ecba"
                },
            };
            characters.ForEach(singleCharacter => context.Characters.AddOrUpdate(c => c.Name, singleCharacter));
            context.SaveChanges();

            context.Articles.AddOrUpdate
            (
                a => a.Title,
                new Article
                {
                    Title = "����һ��ҹ���˿��ƴ�ս���������",
                    ShortDescription = "��ϲRandall������ù��",
                    Description = "һƪ���ڵ�һ���˿��ƴ�ս�񽱽��������",
                    Content= "������ĩҹ����е�....",
                    CoverImgUrl = "/ImgRepository/ArticleImgs/1/wallhaven-278958.jpg",
                    IsPublished =true,
                    PostTime=DateTime.Now,
                    ModifyTime=DateTime.Now,
                    CategoryId = context.ArticleCategories.Single(c => c.Name == "���Ϣ").Id,
                    Category= context.ArticleCategories.Single(c => c.Name == "���Ϣ"),
                    //Dialogs=new List<ArticleDialog>
                    //{
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                    //        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                    //        Message="�����𣿵����𣿻��ǳ�����أ�",
                    //        SequenceNumber=1
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Chiya"),
                    //        Message="��ͨ��˵����21���ʤ�ɣ��Ͼ�����ʽ���˿��Ʊ���",
                    //        SequenceNumber=2
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        Message="����˵��εĽ�Ʒ���ǡ���",
                    //        SequenceNumber=3
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Chiya"),
                    //        Message="�´ζ���˹���̶Ŀ���ʹ�÷���ͷ���Ļ��ᣡ",
                    //        SequenceNumber=4
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        Message="�����ϻ������ֽ�Ʒ�𣿣�",
                    //        SequenceNumber=5
                    //    }
                    //},
                    Tags = new List<ArticleTag>
                    {
                        context.ArticleTags.Single(p=>p.Name=="����"),
                        context.ArticleTags.Single(p=>p.Name=="ҹ��"),
                    }                 
                }
            );
            context.SaveChanges();

            var articleDialogs = new List<ArticleDialog>
            {
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                        Character=context.Characters.Single(c => c.Name == "Rise"),
                        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                        Message="�����𣿵����𣿻��ǳ�����أ�",
                        SequenceNumber=1
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                        Character=context.Characters.Single(c => c.Name == "Chiya"),
                        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                        Message="��ͨ��˵����21���ʤ�ɣ��Ͼ�����ʽ���˿��Ʊ���",
                        SequenceNumber=2
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                        Character=context.Characters.Single(c => c.Name == "Rise"),
                        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                        Message="����˵��εĽ�Ʒ���ǡ���",
                        SequenceNumber=3
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                        Character=context.Characters.Single(c => c.Name == "Chiya"),
                        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                        Message="�´ζ���˹���̶Ŀ���ʹ�÷���ͷ���Ļ��ᣡ",
                        SequenceNumber=4
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                        Character=context.Characters.Single(c => c.Name == "Rise"),
                        ArticleId=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������").Id,
                        Article=context.Articles.Single(a=>a.Title=="����һ��ҹ���˿��ƴ�ս���������"),
                        Message="�����ϻ������ֽ�Ʒ�𣿣�",
                        SequenceNumber=5
                    }
            };
            articleDialogs.ForEach(articleDialog => context.ArticleDialogs.AddOrUpdate(c => new { c.ArticleId, c.CharacterId, c.SequenceNumber, c.Message }, articleDialog));
            context.SaveChanges();
        }
    }
}
