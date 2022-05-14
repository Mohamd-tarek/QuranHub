

export class State {
  constructor(
    public  currentQuranSura : number = 1,
    public  currentTafseerAndTranSura : number = 1,
    public  currentTafseerAndTranAya : number = 1,
    public  currentNoteSura : number = 1,
    public  currentNoteAya : number = 1,
    public  currentSearch : string = "",
    public  searchForWord :boolean = false,
    public  currentStatisticsPage :number = 1  ) { }
}
