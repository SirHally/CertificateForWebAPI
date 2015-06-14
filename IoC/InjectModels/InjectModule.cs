using DAL.Entity;
using DAL.Repository;
using log4net;
using Ninject.Modules;

namespace IoC.InjectModels
{
    /// <summary>
    ///     Это общие биндинги Ninject 
    /// </summary>
    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEntity>().To<MainContext>();
            // биндинг логера
            Bind<ILog>().ToConstant(LogManager.GetLogger("MainLogger"));

            //Репозитарии
            BindRepository();
        }

        /// <summary>
        /// Биндим репозитарии
        /// </summary>
        private void BindRepository()
        {
            Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
        }


    }
}