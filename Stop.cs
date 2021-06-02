namespace lab4
{
    /// <summary>
    /// A stop for public transport
    /// </summary>
    class Stop
    {
        /// <summary>
        /// Name of the stop
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>        
        public Stop(string name) {
            this.Name = name;
        }
    }

}