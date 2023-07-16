import { AfterViewInit, Renderer2, Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../../abstractions/repositories/quranRepository";
import { Quran } from "../../../models/quran/quran.model";
import { StateService } from '../../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Subscription } from "rxjs";
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: "quran",
  templateUrl: "quran.component.html"
})

export class QuranComponent implements OnDestroy , AfterViewInit {
  
  dataLoaded : boolean = false;
  sura!: Quran[];
  currentQuranSura : number = 0;
  currentLanguage: string = "arabic";
  subscription: Subscription;
  qurans!: any;
  ayaIndex!:number;
  
  constructor(
    private repo: QuranRepository,
    private stateService: StateService,
    private activeRoute: ActivatedRoute,
    private renderer: Renderer2) { 
    this.qurans = {"arabic" : this.repo.quran,
                   "english" : this.repo.translation }

   this.subscription =  this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
               .subscribe((newState:any) =>{
                 this.setState(newState)
                });

     this.ayaIndex = this.activeRoute.snapshot.params["ayaIndex"];
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit(): void {

    if(this.ayaIndex)
    {
      setTimeout(() => {
        const element = this.renderer.selectRootElement("#aya" + this.ayaIndex, true); 
        element.scrollIntoView({ behavior: 'smooth' }); 
      }, 5)
    }
  }
  
  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentQuranSura"] == this.currentQuranSura &&
             newState["currentLanguage"] == this.currentLanguage);
  }

  setState(newState: any):void{
    this.currentQuranSura = newState["currentQuranSura"];
    this.currentLanguage = newState["currentLanguage"];
    this.updateSura();
  }

  updateScroll():void{
    if (this.dataLoaded){
      let position: number =  Number.parseInt(localStorage.getItem("scrollPositon") ?? "1") ;
      var domElement =  document.querySelector(".contents");

      if (domElement != null) {
        domElement.scrollTop = position;  
      } 
    }
  }
 
  savePosition():void{
      localStorage.setItem("scrollPositon", document.querySelector(".contents")!.scrollTop.toString());    
  }
 
  updateSura(){
    this.qurans[this.currentLanguage].subscribe((q:any) =>{
      this.sura = q[this.curSura];
      this.dataLoaded = q.length > 1 ; 

      setTimeout(() => {
        this.updateScroll();
      }, 50);
    });
  }

  get curSura(): number { return this.currentQuranSura; }

  set curSura(value: number) {
    this.currentQuranSura =  value;
    let state: any  = {"currentQuranSura": this.currentQuranSura };
    this.stateService.next(state);    
  }


  get suras(){
    return this.repo.suras.getValue();
  }
  
  next(){
    if(this.curSura < 115){
      this.curSura++;
    }
  }

  prev(){
    if(this.curSura > 1 ){
      this.curSura--;
    }
  }

}
