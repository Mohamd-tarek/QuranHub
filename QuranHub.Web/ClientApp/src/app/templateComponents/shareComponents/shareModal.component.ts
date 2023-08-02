import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import {Privacy } from 'src/app/models/post/post.model';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: "shareModal",
  templateUrl: "shareModal.component.html"
})

export class ShareModalComponent  implements OnInit{
 
  verseId! :number;
  user: UserBasicInfo;
  text!:string;
  submitted:boolean = false;
  privacy: Privacy = Privacy.Public;
  addAya:boolean = false;

  @Input()
  post!: any;

  @Input()
  repository!: any;

  @Output()
  shareDoneEvent = new EventEmitter();

  constructor(public userService: UserService) {
    this.user = userService.getUser() as UserBasicInfo;
              }
  ngOnInit(): void {
    this.repository.verses.subscribe((data: any) => {
      this.verseId = data[this.post.verse.sura][this.post.verse.aya - 1].verseId;
     });
  }
  
  addingAya(){
    this.addAya = true;
  }
  
  chooseAyaEvent(verse: any){
    this.verseId = verse.verseId;
  }

  choosePrivacyEvent(privacy: any){
    console.log(typeof(privacy));
    this.privacy = privacy;
    console.log(this.privacy);
  }

  shareDone(){
    this.shareDoneEvent.emit();
  }

  sharingPost(){
    this.submitted = true;
    console.log(this.verseId + " - " + this.user.id + " - "+  this.text + " -", this.privacy)
    this.repository.sharePost(this.verseId, this.user.id, this.text, this.privacy , this.post.postId).subscribe((post:any) => {
         if(post !== null){
          this.submitted = false;
          this.shareDone();
          }
     })
  }
}
