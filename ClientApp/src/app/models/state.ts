

export class State {
  constructor(
    public  currentSura : number = 1,
    public  currentAya : number = 1,
    public  currentSearch : string = "",
    public  searchForWord :boolean = false,
    public  currentStatisticsPage :number = 1 ) { }
}
