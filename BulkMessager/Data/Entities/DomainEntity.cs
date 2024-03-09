using System.ComponentModel.DataAnnotations.Schema;

namespace BulkMessager.Data.Entities {

    /// <summary>
    /// Base class for all Micro domain entities
    /// </summary>
    /// <typeparam name="T">Type of the unique identifier for the domain entity</typeparam>
    public abstract class DomainEntity<T> : IDeleted {
        /// <summary>
        /// Gets or sets the unique Id of the entity in the underlying data store.
        /// </summary>
        [Column("Msg_Id")]
        public T Id { get; set; }

        /// <summary>
        /// Soft delete an object by marking it as deleted
        /// </summary>
        [Column("Msg_Deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Checks if the current domain entity has an identity.
        /// </summary>
        /// <returns>True if the domain entity has no identity yet, false otherwise.</returns>
        public bool IsNew() => default(T) == null || Id.Equals(default(T));

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The object to compare with the current object. 
        /// </param>
        public override bool Equals(object obj) {

            if (obj is not DomainEntity<T>)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = (DomainEntity<T>)obj;

            if (item.IsNew() || IsNew())
                return false;

            return item.Id.Equals(Id);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
            => !IsNew() && Id != null ? Id.GetHashCode() ^ 31 : ToString().GetHashCode();

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="thisEntity">The first instance to compare.</param>
        /// <param name="thatEntity">The second instance to compare.</param>
        public static bool operator ==(DomainEntity<T> thisEntity, DomainEntity<T> thatEntity)
            => thisEntity?.Equals(thatEntity) ?? Equals(thatEntity, null);

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="thisEntity">The first instance to compare.</param>
        /// <param name="thatEntity">The second instance to compare.</param>
        /// <returns>False when the objects are the same, true otherwise.</returns>
        public static bool operator !=(DomainEntity<T> thisEntity, DomainEntity<T> thatEntity)
            => !(thisEntity == thatEntity);
    }
}
