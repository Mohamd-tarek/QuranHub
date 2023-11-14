import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AyaComponent } from "./aya/aya.component";
import { InlineAyaComponent } from "./inline-aya/inline-aya.component";
import { LoadingComponent } from "./loading/loading.component";
import { ProfilePictureComponent } from "./profile-picture/profile-picture.component";
import { RectanglePictureComponent } from "./rectangle-picture/rectangle-picture.component";
import { LinkElementComponent } from "./link-element/link-element.component";
import { RouterModule } from "@angular/router";
import { AnchorListComponent } from "./anchor-list/anchor-list.component";
import { SpinnerComponent } from "./spinner/spinner.component";
import { TextWraperComponent } from "./text-wraper/text-wraper.component";
import { UploadFileComponent } from "./upload-file/upload-file.component";
import { AyaInfoComponent } from "./aya-info/aya-info.component";
import { AyaCardComponent } from "./aya-card/aya-card.component";
import { AyaSetContainerComponent } from "./aya-set-container/aya-set-container.component";
import { UserInfoComponent } from "./user-Info/user-Info.component";
import { NavComponent } from "./nav/nav.component";
import { SideNavComponent } from "./side-nav/side-nav.component";
import { TextInputComponent } from "./text-Input/text-Input.component";
import { UserSetContainerComponent } from "./user-set-container/user-set-container.component";
import { PreviousPointerComponent } from "./previous-pointer/previous-pointer.component";
import { NextPointerComponent } from "./next-pointer/next-pointer.component";
import { ExternalFormComponent } from "./external-form/external-form.component";
import { ChooseAyaComponent } from "./choose-aya/choose-aya.component";
import { ChoosePrivacyComponent } from "./choose-privacy/choose-privacy.component";
import { ModalComponent } from "./modal/modal.component";
import { TabledContainerComponent } from "./tabled-container/tabled-container.component";
import { PaginationComponent } from "./pagination/pagination.component";
import { DateTimeComponent } from "./dateTime/dateTime.component";
import { VideoInfoComponent } from "./video-info/video-info.component";
import { VideosContainerComponent } from "./videos-container/videos-container.component";
import { VideoViewerComponent } from "./video-viewer/video-viewer.component";
import { PlayListComponent } from "./play-list/play-list.component";
import { PlayListsInfoComponent } from "./play-lists-info/play-lists-info.component";
import { PlayListInfoComponent } from "./play-list-lnfo/play-list-lnfo.component";
import { PostComponent } from "./post-components/post.component";
import { SharedPostComponent } from "./post-components/shared-post.component";
import { CommentCountComponent } from "./comment-components/comment-count.component";
import { CommentContainerComponent } from "./comment-components/comment-container.component";
import { CommentComponent } from "./comment-components/comment.component";
import { ViewMoreCommentsComponent } from "./comment-components/view-more-comments.component";
import { CommentOwnerInfo } from "./comment-components/comment-owner-info.component";
import { CommentText } from "./comment-components/comment-text.component";
import { WritingCommentComponent } from "./comment-components/writing-comment.component";
import { AddCommentComponent } from "./comment-components/add-comment.component";
import { LikeCountComponent } from "./like-components/like-count.component";
import { LikesModalComponent } from "./like-components/likes-modal.component";
import { CommentLikesModalComponent } from "./comment-components/comment-likes-modal.component";
import { LikeComponent } from "./like-components/like.component";
import { ShareCountComponent } from "./share-components/share-count.component";
import { SharesModalComponent } from "./share-components/shares-modal.component";
import { ShareComponent } from "./share-components/share.component";
import { PostOwnerInfoComponent } from "./post-components/post-owner-nfo.component";
import { PostOwnerOptionsComponent } from "./post-components/post-owner-options.component";
import { PostTextComponent } from "./post-components/post-text.component";
import { TextAndAyaComponent } from "./post-components/text-and-aya.component";
import { EditPostComponent } from "./post-components/edit-post.component";
import { ShareModalComponent } from "./share-components/share-modal.component";
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
