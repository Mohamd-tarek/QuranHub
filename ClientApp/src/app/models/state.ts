

export class State {
  constructor(
    public authenticated: boolean = false,
    public overviewMode: boolean = false,
    public currentQuranSura: number = 1,
    public currentTafseerAndTranSura: number = 1,
    public currentTafseerAndTranAya: number = 1,
    public currentNoteSura: number = 1,
    public currentNoteAya: number = 1,
    public currentSearch: string = "",
    public searchForWord: boolean = false,
    public showLetters: boolean = false,
    public currentStatisticsPage:number = 1,
    public currentMindMapSura: number = 1  ) { }
}
