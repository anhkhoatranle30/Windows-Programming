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
    public class TripImagePath
    {
        public static string toAbsolutePath(string relativePath, int tripID)
        {
            string result = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Trips\\" + tripID.ToString() + "\\" + relativePath;
            return result;
        }
    }
    class TripImagesDAOsqlserver : TripImagesDAO
    {
        public override BindingList<TRIPIMAGE> GetAll()
        {
            var db = new WP_Project2_WeSplitEntities();
            var result = new BindingList<TRIPIMAGE>(db.TRIPIMAGES.ToList());
            return result;
        }
        public BindingList<TRIPIMAGE> GetTripImagesByTripID(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var trips = db.TRIPs.ToList();
            var tripimages = db.TRIPIMAGES.ToList();
            var query = trips
                .Join(tripimages,
                    t => t.TripID,
                    i => i.TripID,
                    (t, i) => new { Image = i, TripID = t.TripID })
                .Where(r => r.TripID == tripID)
                .Select(r => r.Image).ToList();
            var result = new BindingList<TRIPIMAGE>();
            foreach(var image in query )
            {
                result.Add(new TRIPIMAGE() { TripID = tripID, 
                                            Path = TripImagePath.toAbsolutePath(image.Path, tripID), 
                                            TRIP = image.TRIP, 
                                            TripImageID = (int)image.TripID });
            }
            return result;
        }
        public string GetTripAvatar(int tripID)
        {
            var db = new WP_Project2_WeSplitEntities();
            var imageList = db.TRIPIMAGES.ToList();
            var result = imageList[0].Path;
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
            var old_info = db.TRIPIMAGES.Find(tripimage.TripImageID);
            old_info = tripimage;
            db.SaveChanges();
        }
    }
}
