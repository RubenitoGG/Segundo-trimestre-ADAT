using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		protected Context context;
		DbSet<TEntity> dbSet;

		public GenericRepository(Context context)
		{
			this.context = context;
			this.dbSet = context.Set<TEntity>();
		}

		public void Add(TEntity entity)
		{
            try
            {
                dbSet.Add(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error al añadir. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

		public void Update(TEntity entity)
		{
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error al modificar. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

		public void Delete(TEntity entity)
		{
            try
            {
                dbSet.Remove(entity);
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error al eliminar. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

		public void DeleteGroup(Expression<Func<TEntity, bool>> predicate)
		{
            try
            {
                var entities = dbSet.Where(predicate).ToList();
                entities.ForEach(x => context.Entry(x).State = EntityState.Deleted);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error eliminar en grupo. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

		public TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.FirstOrDefault(predicate);
		}

		public List<TEntity> GetGroup(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.Where(predicate).ToList();
		}

		public List<TEntity> GetAll()
		{
			return dbSet.ToList();
		}

		public virtual IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			foreach (var includeProperty in includeProperties.Split
			(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}
	}
}
