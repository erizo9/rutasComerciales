class Visit
    {
         public DateTime Date { get; set; }
        public string? Location { get; set; }
        public bool Completed { get; set; }
        public string? Purpose { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Expenses { get; set; }
        public void ViewVisit()
        {
            Console.WriteLine($"{Date.ToShortDateString()} - {Location} {(Completed ? "(Completada)" : "(Pendiente)")}");
        }

        public void MarkCompleted()
        {
            Completed = true;
            Console.WriteLine("Visita marcada como realizada.");
        }
    }
