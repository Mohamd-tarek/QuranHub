import { QuranBase } from "./quranBase.model";

export class Quran extends QuranBase {
  constructor(
    public id: number,
    public index: number,
    public sura: number,
    public aya: number,
    public text: string) {

    super(index, sura, aya, text);
  }
 
}