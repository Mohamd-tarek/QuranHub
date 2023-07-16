import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { CommonModule } from '@angular/common';
import { PostComponent } from "./postComponents/post.component";
import { SharedPostComponent } from "./postComponents/sharedPost.component";
import { CommentCountComponent } from "./commentComponents/commentCount.component";
import { CommentContainerComponent } from "./commentComponents/commentContainer.component";
import { CommentComponent } from "./commentComponents/comment.component";
import { ViewMoreCommentsComponent } from "./commentComponents/viewMoreComments.component";
import { CommentOwnerInfo } from "./commentComponents/commentOwnerInfo.component";
import { CommentText } from "./commentComponents/commentText.component";
import { WritingCommentComponent } from "./commentComponents/writingComment.component";
import { AddCommentComponent } from "./commentComponents/addComment.component";
import { LikeCountComponent } from "./likeComponents/likeCount.component";
import { LikesModalComponent } from "./likeComponents/likesModal.component";
import { LikeComponent } from "./likeComponents/like.component";
import { ShareCountComponent } from "./shareComponents/shareCount.component";
import { ShareComponent } from "./shareComponents/share.component";
import { PostOwnerInfoComponent } from "./postComponents/postOwnerInfo.component";
import { PostOwnerOptionsComponent } from "./postComponents/postOwnerOptions.component";
import { PostTextComponent } from "./postComponents/postText.component";
import { TextAndAyaComponent } from "./postComponents/textAndAya.component";
import { EditPostComponent } from "./postComponents/editPost.component";
import { ShareModalComponent } from "./shareComponents/shareModal.component";
import { TemplateModule } from "../templateComponents/template.module";

@NgModule({   
  imports: [
    RouterModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    TemplateModule
  ],
  declarations: [
    PostComponent,
    SharedPostComponent,
    CommentContainerComponent,
    CommentComponent,
    ViewMoreCommentsComponent,
    CommentOwnerInfo,
    CommentText,
    WritingCommentComponent,
    LikeCountComponent,
    LikesModalComponent,
    CommentCountComponent,
    ShareCountComponent,
    LikeComponent,
    AddCommentComponent,
    ShareComponent,
    PostOwnerInfoComponent,
    PostOwnerOptionsComponent,
    PostTextComponent,
    TextAndAyaComponent,
    EditPostComponent,
    ShareModalComponent
  ],

  providers: [],

  exports: [
    PostComponent,
    SharedPostComponent
  ]
})

export class PostModule {}
