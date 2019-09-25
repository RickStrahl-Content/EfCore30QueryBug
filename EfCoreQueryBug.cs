using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumViewerBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreQueryBug
{
    [TestClass]
    public class EfCoreQueryBug
    {
        [TestMethod]
        public void EfCoreQueryWithSqLiteDataBug()
        {
            var Context = new AlbumViewerContext() {useSqLite = true};


            IQueryable<Album> albums = Context.Albums
                .Include(ctx => ctx.Tracks)
                .Include(ctx => ctx.Artist)
                .OrderBy(alb => alb.Title);


            var list = albums.ToList();

            var listUninitialized = list.Where(a => a.Artist.Id == 0).ToList();

            Assert.IsTrue(list.Count > 80, "Total number of records should be greater than 80");
            Assert.IsTrue(listUninitialized.Count == 0,
                $"There are {listUninitialized.Count} uninitialized Artist records");
        }

        [TestMethod]
        public void ImportAlbumData()
        {
            var Context = new AlbumViewerContext()
            {
                useSqLite = false,
                ConnectionString = "server=.;database=AlbumViewer2;integrated security=true;MultipleActiveResultSets=true;App=AlbumViewer"
            };
            AlbumViewerDataImporter.EnsureAlbumData(Context, Path.GetFullPath("./albums.js"));

        }
    }
}