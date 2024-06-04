
namespace Influence
{
    /// <summary>Base class for all objects Influence can reference.</summary>
    public class Object
    {
        /// <summary>Gets or sets the name of the object.</summary>
        public virtual string name { get; set; } = "Default Object";

        // Static counter to generate unique instance IDs.
        static int nextInstanceID = 1;
        /// <summary>Unique Identifier for each instance of the Object class.</summary>
        protected readonly int instanceID;

        /// <summary>Initializes a new instance of the Object class with a default name.</summary>
        public Object()
        {
            instanceID = nextInstanceID++;
        }

        /// <summary>Initializes a new instance of the Object class with a specified name.</summary>
        /// <param name="name">The name of the object.</param>
        public Object(string name)
        {
            instanceID = nextInstanceID++;
            this.name = name;
        }

        #region Common Overrides (ToString, GetHashCode, Equals)

        /// <summary>Returns the name of the current object.</summary>
        public override string ToString() => name;

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object other)
        {
            if (!(other is Object otherObject)) 
                return false;

            return CompareObjects(this, otherObject);
        }

        public override int GetHashCode() => instanceID;

        #endregion

        #region Functions

        /// <summary>Compares two Object instances for equality based on their instance IDs.</summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        static bool CompareObjects(Object a, Object b)
        {
            bool aNull = (object)a == null;
            bool bNull = (object)b == null;

            if (aNull && bNull) return true;
            if(aNull != bNull) return false;

            return a.instanceID == b.instanceID;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implicit conversion operator to convert an Object instance to a boolean.
        /// Returns true if the object exists (is not null).
        /// </summary>
        /// <param name="exists">The object to check for existence.</param>
        public static implicit operator bool(Object exists) => !CompareObjects(exists, null);

        public static bool operator ==(Object a, Object b) => CompareObjects(a, b);

        public static bool operator !=(Object a, Object b) => !CompareObjects(a, b);

        #endregion

    }
}
