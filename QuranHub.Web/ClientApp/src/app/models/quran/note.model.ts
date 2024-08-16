import { QuranBase } from "./quranBase.model";

export class Note extends QuranBase {
  constructor(
    public noteId: number,
    public index: number,
    public sura: number,
    public aya: number,
    public text: string) {

    super(index, sura, aya, text);
  }
}
