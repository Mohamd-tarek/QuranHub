import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AyaComponent } from "./aya/aya.component";
import { InlineAyaComponent } from "./inlineAya/inlineAya.component";
import { LoadingComponent } from "./loading/loading.component";
import { ProfilePictureComponent } from "./profilePicture/profilePicture.component";
import { RectanglePictureComponent } from "./rectanglePicture/rectanglePicture.component";
import { LinkElementComponent } from "./linkElement/linkElement.component";
import { RouterModule } from "@angular/router";
import { AnchorListComponent } from "./anchorList/anchorList.component";
import { SpinnerComponent } from "./spinner/spinner.component";
import { TextWraperComponent } from "./textWraper/textWraper.component";
import { UploadFileComponent } from "./uploadFile/uploadFile.component";
import { AyaInfoComponent } from "./ayaInfo/ayaInfo.component";
import { AyaCardComponent } from "./ayaCard/ayaCard.component";
import { AyaSetContainerComponent } from "./ayaSetContainer/ayaSetContainer.component";
import { UserInfoComponent } from "./userInfo/userInfo.component";
import { NavComponent } from "./nav/nav.component";
import { SideNavComponent } from "./sideNav/sideNav.component";
import { TextInputComponent } from "./textInput/textInput.component";
import { UserSetContainerComponent } from "./userSetContainer/userSetContainer.component";
import { PreviousPointerComponent } from "./previousPointer/previousPointer.component";
import { NextPointerComponent } from "./nextPointer/nextPointer.component";
import { ExternalFormComponent } from "./externalForm/externalForm.component";
import { ChooseAyaComponent } from "./chooseAya/chooseAya.component";
import { ChoosePrivacyComponent } from "./choosePrivacy/choosePrivacy.component";
import { ModalComponent } from "./modal/modal.component";
import { TabledContainerComponent } from "./tabledContainer/tabledContainer.component";
import { PaginationComponent } from "./pagination/pagination.component";
import { DateTimeComponent } from "./dateTime/dateTime.component";
import { VideoInfoComponent } from "./videoInfo/videoInfo.component";
import { VideosContainerComponent } from "./videosContainer/videosContainer.component";
import { VideoViewerComponent } from "./videoViewer/videoViwer.component";
import { PlayListComponent } from "./playList/playList.component";
import { PlayListsInfoComponent } from "./playListsInfo/playListsInfo.component";
import { PlayListInfoComponent } from "./playListInfo/playListInfo.component";
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
import { CommentLikesModalComponent } from "./commentComponents/commentLikesModal.component";
import { LikeComponent } from "./likeComponents/like.component";
import { ShareCountComponent } from "./shareComponents/shareCount.component";
import { SharesModalComponent } from "./shareComponents/sharesModal.component";
import { ShareComponent } from "./shareComponents/share.component";
import { PostOwnerInfoComponent } from "./postComponents/postOwnerInfo.component";
import { PostOwnerOptionsComponent } from "./postComponents/postOwnerOptions.component";
import { PostTextComponent } from "./postComponents/postText.component";
import { TextAndAyaComponent } from "./postComponents/textAndAya.component";
import { EditPostComponent } from "./postComponents/editPost.component";
import { ShareModalComponent } from "./shareComponents/shareModal.component";
import { onClickOutsideHideDirective } from "./onClickOutsideHide.directive";
import { DragulaModule } from 'ng2-dragula';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({   
  imports: [
    BrowserModule,
    RouterModule,
    DragulaModule,
    FormsModule,
    ReactiveFormsModule
  ],

  declarations: [
    AyaComponent,
    InlineAyaComponent,
    LoadingComponent,
    ProfilePictureComponent,
    SpinnerComponent,
    RectanglePictureComponent,
    LinkElementComponent,
    AnchorListComponent,
    TextWraperComponent,
    UploadFileComponent,
    AyaInfoComponent,
    AyaCardComponent,
    AyaSetContainerComponent,
    UserInfoComponent,
    NavComponent,
    SideNavComponent,
    TextInputComponent,
    UserSetContainerComponent,
    PreviousPointerComponent,
    NextPointerComponent,
    ExternalFormComponent,
    ChooseAyaComponent,
    ChoosePrivacyComponent,
    ModalComponent,
    TabledContainerComponent,
    PaginationComponent,
    DateTimeComponent,
    VideoInfoComponent,
    VideosContainerComponent,
    VideoViewerComponent,
    PlayListInfoComponent,
    PlayListsInfoComponent,
    PlayListComponent,
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
    CommentLikesModalComponent,
    CommentCountComponent,
    ShareCountComponent,
    SharesModalComponent,
    LikeComponent,
    AddCommentComponent,
    ShareComponent,
    PostOwnerInfoComponent,
    PostOwnerOptionsComponent,
    PostTextComponent,
    TextAndAyaComponent,
    EditPostComponent,
    ShareModalComponent,
    onClickOutsideHideDirective
  ],

  providers: [],

  exports: [
    AyaComponent,
    InlineAyaComponent,
    LoadingComponent,
    ProfilePictureComponent,
    SpinnerComponent,
    RectanglePictureComponent,
    LinkElementComponent,
    AnchorListComponent,
    TextWraperComponent,
    UploadFileComponent,
    AyaInfoComponent,
    AyaCardComponent,
    AyaSetContainerComponent,
    UserInfoComponent,
    NavComponent,
    SideNavComponent,
    TextInputComponent,
    UserSetContainerComponent,
    PreviousPointerComponent,
    NextPointerComponent,
    ExternalFormComponent,
    ChooseAyaComponent,
    ChoosePrivacyComponent,
    ModalComponent,
    TabledContainerComponent,
    PaginationComponent,
    DateTimeComponent,
    VideoInfoComponent,
    VideosContainerComponent,
    VideoViewerComponent,
    PlayListInfoComponent,
    PlayListsInfoComponent,
    PlayListComponent,
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
    CommentLikesModalComponent,
    CommentCountComponent,
    ShareCountComponent,
    SharesModalComponent,
    LikeComponent,
    AddCommentComponent,
    ShareComponent,
    PostOwnerInfoComponent,
    PostOwnerOptionsComponent,
    PostTextComponent,
    TextAndAyaComponent,
    EditPostComponent,
    ShareModalComponent,
    onClickOutsideHideDirective
  ]
})

export class TemplateModule {}
