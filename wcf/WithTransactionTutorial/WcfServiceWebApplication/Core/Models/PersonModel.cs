using System.Runtime.Serialization;
using Core.Models.Enums;

namespace Core.Models
{
    /// <summary>
    /// Person data object for represent person data received from db
    /// </summary>
    [DataContract]
    public class PersonModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        [DataMember]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the type of the gender.
        /// </summary>
        /// <value>
        /// The type of the gender.
        /// </value>
        [DataMember]
        public GenderType GenderType { get; set; }
    }
}