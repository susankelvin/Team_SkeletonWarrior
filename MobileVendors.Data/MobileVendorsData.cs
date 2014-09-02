namespace MobileVendors.Data
{
    using System;
    using System.Collections.Generic;

    using MobileVendors.Data.Repositories;
    using MobileVendors.Models;

    public class MobileVendorsData : IMobileVendorsData
    {
        private readonly IMobileVendorsDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public MobileVendorsData() : this(new MobileVendorsDbContext())
        {
        }

        public MobileVendorsData(IMobileVendorsDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Service> Services
        {
            get
            {
                return this.GetRepository<Service>();
            }
        }

        public IGenericRepository<Vendor> Vendors
        {
            get
            {
                return this.GetRepository<Vendor>();
            }
        }

        public IGenericRepository<Subscription> Subscriptions
        {
            get
            {
                return this.GetRepository<Subscription>();
            }
        }

        public IGenericRepository<Store> Stores
        {
            get
            {
                return this.GetRepository<Store>();
            }
        }

        public IGenericRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IGenericRepository<Town> Towns
        {
            get
            {
                return this.GetRepository<Town>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);             
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}