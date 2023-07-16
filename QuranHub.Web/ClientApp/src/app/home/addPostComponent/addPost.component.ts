import { Component, Output, EventEmitter } from '@angular/core';
import { HomeService } from '../../abstractions/services/homeService';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { Post } from 'src/app/models/post/post.model';
import { Privacy } from 'src/app/models/post/post.model';

@Component({
  selector: "addPost",
  templateUrl: "addPost.component.html"
})

export class AddPostComponent {
  
   verseId! :number;
   user: UserBasicInfo;
   text!:string;
   WritingPost: boolean = false;
   submitted:boolean = false;
   privacy: Privacy = Privacy.Public;

   @Output()
   postAddedEvent = new EventEmitter<Post>();

   constructor(
      public homeDataService: HomeService,
      public userService: UserService) {
      this.user = userService.getUser() as UserBasicInfo;
                }
  
   chooseAyaEvent(verse: any){
      this.verseId = verse.verseId;
   }

   choosePrivacyEvent(privacy: Privacy){
      this.privacy = privacy;
   }

   startWritingPost(){
      this.WritingPost = true;
   }

   CancelWritingPost(){
      this.WritingPost = false;
   }

   AddingPost(){
      this.submitted = true;
      console.log(this.verseId + " - " + this.user.id + " - "+  this.text + " -", this.privacy)
      this.homeDataService.addPost(this.verseId, this.user.id, this.text, this.privacy ).subscribe(post => {
           if(post !== null){
            this.submitted = false;
            this.WritingPost = false;
            this.postAddedEvent.emit(post);
           }
       })
   }
}
