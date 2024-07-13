import { Hadith } from "./hadith.model";


export class Section {
  constructor(
    public sectionId: number,
    public hadithNumberStart: number,
    public hadithNumberEnd: number,
    public name: string,
    public hadiths: Hadith[]) {
  }
}
