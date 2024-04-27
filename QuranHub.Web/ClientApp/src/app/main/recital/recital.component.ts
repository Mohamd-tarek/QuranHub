import { AfterViewInit, Renderer2, Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Quran } from "../../models/quran/quran.model";
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Subscription } from "rxjs";
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: "recital",
  templateUrl: "recital.component.html"
})

export class RecitalComponent implements OnDestroy , AfterViewInit {
  
  dataLoaded : boolean = false;
  sura!: Quran[];
  currentRecitalSura : number = 0;
  subscription: Subscription;
  qurans!: any;
  ayaIndex!:number;
  
  constructor(
    private repo: QuranRepository,
    private stateService: StateService,
    private activeRoute: ActivatedRoute,
    private renderer: Renderer2) { 
    this.qurans = this.repo.quran;

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
    return ( newState["currentQuranSura"] == this.currentRecitalSura);
  }

  setState(newState: any):void{
    this.currentRecitalSura = newState["currentRecitalSura"];
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
    this.qurans.subscribe((q:any) =>{
      this.sura = q[this.curSura];
      this.dataLoaded = q.length > 1 ; 

      setTimeout(() => {
        this.updateScroll();
      }, 50);
    });
  }

  get curSura(): number { return this.currentRecitalSura; }

  set curSura(value: number) {
    this.currentRecitalSura =  value;
    let state: any  = {"currentRecitalSura": this.currentRecitalSura };
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
