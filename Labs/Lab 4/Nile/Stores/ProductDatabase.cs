/*
 * Matthew McNatt
 * ITSE 1430
 * 4/17/2019
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nile.Stores
{
    /// <summary>Base class for product database.</summary>
    public abstract class ProductDatabase : IProductDatabase
    {        
        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public Product Add ( Product product )
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));


            ObjectValidator.Validate(product);

            //products must be unique
            var existing = FindByName(product.Name);
            if (existing != null)
                throw new Exception("Product must be unique.");

            //Emulate database by storing copy
            return AddCore(product);
        }

        /// <summary>Get a specific product.</summary>
        /// <returns>The product, if it exists.</returns>
        public Product Get ( int id )
        {
            if(id > 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

            return GetCore(id);
        }
        
        /// <summary>Gets all products.</summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetAll ()
        {
            return GetAllCore();
        }
        
        /// <summary>Removes the product.</summary>
        /// <param name="id">The product to remove.</param>
        public void Remove ( int id )
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

            RemoveCore(id);
        }
        
        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        public Product Update ( Product product )
        {
            //makes sure product is not null
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            //Get existing product
            var existing = GetCore(product.Id);

            //checks to make sure product being updated exists
            if (existing == null)
                throw new Exception("product does no exist");


            ObjectValidator.Validate(product);

            //ensure product is unique after update
            var sameName = FindByName(product.Name);
            if (sameName != null && sameName.Id != existing.Id)
                throw new Exception("products must be unique.");

            return UpdateCore(existing, product);
        }


        #region Protected Members

        protected virtual Product FindByName( string name )
        {

            return (from product in GetAllCore()
                    where String.Compare(product.Name, name, true) == 0
                    select product).FirstOrDefault();

        }

        protected abstract Product GetCore( int id );

        protected abstract IEnumerable<Product> GetAllCore();

        protected abstract void RemoveCore( int id );

        protected abstract Product UpdateCore( Product existing, Product newItem );

        protected abstract Product AddCore( Product product );
        #endregion
    }
}
