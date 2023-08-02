import { Component} from '@angular/core';
import { Post } from 'src/app/models/post/post.model';
import { ActivatedRoute } from '@angular/router';
import { PostRepository } from "../../abstractions/repositories/postRepository";
import { NotificationRepository } from '../../abstractions/repositories/notificationRepository';
import { Notification } from 'src/app/models/notification.model';

@Component({
  selector: "postViewer",
  templateUrl: "postViewer.component.html"
})

export class PostViewerComponent {
  
  notificationId:number;
  notification!:Notification;
  post!:Post;
  dataLoaded:boolean = false;

 

  constructor(
    private activeRoute: ActivatedRoute,
    public postDataRepository: PostRepository,
    public notificationRepository: NotificationRepository) {

    this.notificationId = this.activeRoute.snapshot.params["notificationId"];
    console.log("this.notificationId : " + this.notificationId);
    this.notificationRepository.getNotificationById(this.notificationId).subscribe(notification => {
      this.notification = notification;
      console.log("this.notification : " + this.notification.postId);
        if(notification.commentId) {
          this.postDataRepository.getPostByIdWithSpecificComment(this.notification.postId, this.notification.commentId).subscribe(post => {
            const index = post.comments.findIndex(comment => comment.commentId == this.notification.commentId);
            const temp = post.comments[0];
            post.comments[0] = post.comments[index];
            post.comments[index] = temp;
            this.post = post;
            this.dataLoaded = true;
          })
        }
        else{
          this.postDataRepository.getPostById(this.notification.postId).subscribe(post => {
            this.post = post;
            this.dataLoaded = true;
          })

        }
      })
      
    }

        
}
