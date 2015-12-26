using CRIMAS.Repository.contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRIMAS.Repository.artifacts
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private Models.CrimasDb _db;
        private DbSet<T> _entity;
        public Repository()
        {
            _db = new CRIMAS.Models.CrimasDb();
            _entity = _db.Set<T>();
        }
        public void Add(T obj)
        {
            if (obj != null)
            {
                _entity.Add(obj);
                _db.SaveChanges();
            }
        }

        public IList<T> GetAll()
        {
            return _entity.ToList();
        }

        public T GetById(object id)
        {
            return _entity.Find(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public IList<T> FindAll(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public T Update(T updated, int Key)
        {
            var updating = _entity.Find(Key);
            if (updated != null)
            {
                updating = updated;
                Commit();

                return updated;
            }
            return updated;
        }

        public bool RemoveById(object id)
        {
            var entity = _entity.Find(id);
            if (entity != null)
            {
                _entity.Remove(entity);
                Commit();

                return true;
            }
            return false;
        }

        public void Commit()
        {
            _db.SaveChanges();

        }

        public int count()
        {
            return _entity.Count();
        }


        //public void ExportData(IList<T> obj, string filename)
        //{
        //    GridView gv = new GridView();
        //    gv.DataSource = obj;
        //    gv.DataBind();

        //    Response.AddHeader("content-disposition", "attachment; filename="+filename+".xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    gv.RenderControl(htw);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();
        //}
    }
}