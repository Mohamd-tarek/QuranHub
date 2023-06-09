﻿namespace ServerApp.Models
{
    public class State
    {
        public bool authenticated {get; set;}
        public bool overviewMode {get ; set;}
        public int currentQuranSura { get; set; }
        public int currentTafseerAndTranSura {get; set; }
        public int currentTafseerAndTranAya  {get; set; }
        public int currentNoteSura {get; set; }
        public int currentNoteAya  {get; set; }
        public string currentSearch { get; set; }
        public bool searchForWord { get; set; }
        public bool showLetters {get; set;}
        public int currentStatisticsPage { get; set; }
    }


}
