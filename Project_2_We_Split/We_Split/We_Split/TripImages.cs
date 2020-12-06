using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split
{
    class TripImages
    {
    }
    public abstract class TripImagesDAO
    {
        public abstract BindingList<TRIPIMAGE> GetAll();
        public abstract void Add(TRIPIMAGE tripimage);
        public abstract void Delete(TRIPIMAGE tripimage);
        public abstract void Update(TRIPIMAGE tripimage);
    }

    class TripImagesDAOsqlserver : TripImagesDAO
    {
        public override BindingList<TRIPIMAGE> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<TRIPIMAGE>(db.TRIPIMAGES.ToList());
            return result;
        }
        public override void Add(TRIPIMAGE tripimage)
        {
            var db = new WP_Project2_WeSplitEntities();
            db.TRIPIMAGES.Add(tripimage);
            db.SaveChanges();
        }
        public override void Delete(TRIPIMAGE tripimage)
        {
            var db = new WP_Project2_WeSplitEntities();

            db.TRIPIMAGES.Remove(tripimage);
            db.SaveChanges();
        }
        public override void Update(TRIPIMAGE tripimage)
        {
            var db = new WP_Project2_WeSplitEntities();
            var old_info = db.TRIPIMAGES.Find(tripimage.TripID);
            old_info = tripimage;
            db.SaveChanges();
        }
    }
}
