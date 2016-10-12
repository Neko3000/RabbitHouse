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
                    Name="冷饮",
                    Description="加了椭圆形冰粒的产品",
                    ImgUrl="/ImgRepository/ProductPropertyImgs/ice.png"
                },
                new ProductProperty
                {
                    Name="热饮",
                    Description="经过严密保温的产品",
                    ImgUrl="/ImgRepository/ProductPropertyImgs/hot.png"
                },
                new ProductProperty
                {
                    Name="橘子酱",
                    Description="添加了橘子果酱的产品",
                    PlusPrice=1,
                    ImgUrl="/ImgRepository/ProductPropertyImgs/orange.png"
                },
                new ProductProperty
                {
                    Name="蓝莓酱",
                    Description="添加了蓝莓果酱的产品",
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
                    Name="咖啡",
                    Description="标准咖啡配置"
                },
                new ProductCategory
                {
                    Name="甜点",
                    Description="就如同名字一样加了一些糖"
                },
                new ProductCategory
                {
                    Name="饮品",
                    Description="不是咖啡的饮料都在这了"
                },
                new ProductCategory
                {
                    Name="主食",
                    Description="能吃饱的东西们"
                },
            };
            productCategories.ForEach(singleCategory => context.ProductCategories.AddOrUpdate(p => p.Name, singleCategory));
            context.SaveChanges();

            context.Products.AddOrUpdate
            (
                p=>p.Name,
                new Product
                {
                    Name = "经典摩卡咖啡",
                    ShortDescription = "所谓COFFEEMOCHA，就是那段在办公桌上的午休时光",
                    Description = "「咖啡摩卡，再来一杯~」啊——我们就知道又是到下午茶的时间了。摩卡咖啡（CafeMocha）是一种最古老的咖啡，其历史要追溯到咖啡的起源。它是由意大利浓缩咖啡、巧克力酱、鲜奶油和牛奶混合而成，摩卡得名于有名的摩卡港。十五世纪，整个中东非咖啡国家向外运输业不兴盛，也门摩卡是当时红海附近主要输出一个商港，当时咖啡主要是集中到摩卡港再向外输出的非洲咖啡，都被统称摩卡咖啡。而新兴的港口虽然代替了摩卡港的地位，但是摩卡港时期摩卡咖啡的产地依然保留了下来，这些产地所产的咖啡豆，仍被称为摩卡咖啡豆。",
                    Remark = "经典摩卡咖啡现在正在8折优惠中，外送以及堂食都可享受此次优惠 ※此商品外送时提供纸杯包装",
                    Price = (decimal)12.0,
                    CurrentDiscount = (decimal)0.8,
                    DiscountStartTime = new DateTime(2016, 3, 1, 12, 30, 0),
                    DiscountEndTime = new DateTime(2016, 5, 1, 12, 30, 0),
                    PublishTime = new DateTime(2016, 2, 1),
                    IsSeasonalProduct = false,
                    CategoryId=context.ProductCategories.Single(c=>c.Name== "咖啡").Id,
                    Category=context.ProductCategories.Single(c=>c.Name=="咖啡"),
                    CoverImgUrl= "/ImgRepository/ProductImgs/1/dark-chocolate-mocha-avocado-mousse-4.jpg",
                    Properties = new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="冷饮"),
                        context.ProductProperties.Single(p=>p.Name=="热饮"),
                    }
                },
                new Product
                {
                    Name = "布丁奶酪迷你甜蛋糕",
                    ShortDescription = "当布丁与奶酪碰撞在一起，会是怎样的一番火花？！",
                    Description = "作为本店甜点中的人气商品NO.1，绝对不会令您失望",
                    Remark = "新增了蓝莓酱的选项",
                    Price = (decimal)10.0,
                    PublishTime = new DateTime(2016, 2, 5),
                    IsSeasonalProduct = false,
                    CategoryId = context.ProductCategories.Single(c => c.Name == "甜点").Id,
                    Category = context.ProductCategories.Single(c => c.Name == "甜点"),
                    CoverImgUrl = "/ImgRepository/ProductImgs/2/wallhaven-284723.jpg",
                    Properties =new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="橘子酱"),
                        context.ProductProperties.Single(p=>p.Name=="蓝莓酱"),
                    }
                },
                new Product
                {
                    Name = "枫糖茶",
                    ShortDescription = "不仅仅是枫糖与茶的融合",
                    Description = "枫糖茶有一个优点——它不是很甜！",
                    Remark = "目前仅有大杯装",
                    Price = (decimal)5.0,
                    PublishTime = new DateTime(2016, 5, 1),
                    IsSeasonalProduct = true,
                    SaleStartTime = new DateTime(2016, 6, 10, 0 ,0 ,0),
                    SaleEndTime = new DateTime(2016, 9, 10, 0, 0, 0),
                    CategoryId = context.ProductCategories.Single(c => c.Name == "饮品").Id,
                    Category = context.ProductCategories.Single(c => c.Name == "饮品"),
                    CoverImgUrl = "/ImgRepository/ProductImgs/3/tea-01.jpg",
                    Properties = new List<ProductProperty>
                    {
                        context.ProductProperties.Single(p=>p.Name=="冷饮"),
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
                    Product= context.Products.Single(c => c.Name == "经典摩卡咖啡"),
                    ProductId= context.Products.Single(c => c.Name == "经典摩卡咖啡").Id,
                    RecordTime=DateTime.Now
                },
                new ProductImage
                {
                    Name = "p1-d2.jpg",
                    Url = "/ImgRepository/ProductImgs/1/p1-d2.jpg",
                    Product = context.Products.Single(c => c.Name == "经典摩卡咖啡"),
                    ProductId = context.Products.Single(c => c.Name == "经典摩卡咖啡").Id,
                    RecordTime = DateTime.Now
                },
                new ProductImage
                {
                    Name = "cheese1.jpg",
                    Url = "/ImgRepository/ProductImgs/2/cheese1.jpg",
                    Product = context.Products.Single(c => c.Name == "布丁奶酪迷你甜蛋糕"),
                    ProductId = context.Products.Single(c => c.Name == "布丁奶酪迷你甜蛋糕").Id,
                    RecordTime = DateTime.Now
                },
                new ProductImage
                {
                    Name = "tea1.jpg",
                    Url = "/ImgRepository/ProductImgs/3/tea1.jpg",
                    Product = context.Products.Single(c => c.Name == "枫糖茶"),
                    ProductId = context.Products.Single(c => c.Name == "枫糖茶").Id,
                    RecordTime = DateTime.Now
                }
            );
            context.SaveChanges();

            //article
            var articleCategories = new List<ArticleCategory>
            {
                new ArticleCategory
                {
                    Name="活动信息",
                    Description="店铺中各种活动的公告",
                },
                new ArticleCategory
                {
                    Name="促销信息",
                    Description="对限时特价商品内容的通知",
                },
            };
            articleCategories.ForEach(singleCategory => context.ArticleCategories.AddOrUpdate(p => p.Name, singleCategory));
            context.SaveChanges();

            var articleTags = new List<ArticleTag>
            {
                new ArticleTag
                {
                    Name="夏日",
                    Description="和炎炎夏日有关的文章",
                },
                new ArticleTag
                {
                    Name="咖啡",
                    Description="与咖啡有关系的文章",
                },
                new ArticleTag
                {
                    Name="夜间",
                    Description="重点在夜间的信息",
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
                    Description="真是一个军事爱好者",
                    ImgUrl="/ImgRepository/CharacterImgs/001.jpg",
                    Color="#c7b6ff"
                },
                new Character
                {
                    Name="Chiya",
                    Description="每天都为想出新的厉害菜单而努力着",
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
                    Title = "「第一回夜间扑克牌大战」结果公布",
                    ShortDescription = "恭喜Randall先生夺得桂冠",
                    Description = "一篇关于第一回扑克牌大战获奖结果的文章",
                    Content= "在上周末夜间举行的....",
                    CoverImgUrl = "/ImgRepository/ArticleImgs/1/wallhaven-278958.jpg",
                    IsPublished =true,
                    PostTime=DateTime.Now,
                    ModifyTime=DateTime.Now,
                    CategoryId = context.ArticleCategories.Single(c => c.Name == "活动信息").Id,
                    Category= context.ArticleCategories.Single(c => c.Name == "活动信息"),
                    //Dialogs=new List<ArticleDialog>
                    //{
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                    //        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                    //        Message="轮盘吗？德州吗？还是抽鬼牌呢？",
                    //        SequenceNumber=1
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Chiya"),
                    //        Message="普通来说都是21点决胜吧，毕竟是正式的扑克牌比赛",
                    //        SequenceNumber=2
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        Message="所以说这次的奖品会是——",
                    //        SequenceNumber=3
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Chiya"),
                    //        Message="下次俄罗斯轮盘赌可以使用防弹头盔的机会！",
                    //        SequenceNumber=4
                    //    },
                    //    new ArticleDialog
                    //    {
                    //        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                    //        Character=context.Characters.Single(c => c.Name == "Rise"),
                    //        Message="世界上会有这种奖品吗？！",
                    //        SequenceNumber=5
                    //    }
                    //},
                    Tags = new List<ArticleTag>
                    {
                        context.ArticleTags.Single(p=>p.Name=="夏日"),
                        context.ArticleTags.Single(p=>p.Name=="夜间"),
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
                        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                        Message="轮盘吗？德州吗？还是抽鬼牌呢？",
                        SequenceNumber=1
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                        Character=context.Characters.Single(c => c.Name == "Chiya"),
                        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                        Message="普通来说都是21点决胜吧，毕竟是正式的扑克牌比赛",
                        SequenceNumber=2
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                        Character=context.Characters.Single(c => c.Name == "Rise"),
                        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                        Message="所以说这次的奖品会是——",
                        SequenceNumber=3
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Chiya").Id,
                        Character=context.Characters.Single(c => c.Name == "Chiya"),
                        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                        Message="下次俄罗斯轮盘赌可以使用防弹头盔的机会！",
                        SequenceNumber=4
                    },
                    new ArticleDialog
                    {
                        CharacterId=context.Characters.Single(c => c.Name == "Rise").Id,
                        Character=context.Characters.Single(c => c.Name == "Rise"),
                        ArticleId=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布").Id,
                        Article=context.Articles.Single(a=>a.Title=="「第一回夜间扑克牌大战」结果公布"),
                        Message="世界上会有这种奖品吗？！",
                        SequenceNumber=5
                    }
            };
            articleDialogs.ForEach(articleDialog => context.ArticleDialogs.AddOrUpdate(c => new { c.ArticleId, c.CharacterId, c.SequenceNumber, c.Message }, articleDialog));
            context.SaveChanges();
        }
    }
}
