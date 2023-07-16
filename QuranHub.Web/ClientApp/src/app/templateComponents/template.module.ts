import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AyaComponent } from "./aya/aya.component";
import { InlineAyaComponent } from "./inlineAya/inlineAya.component";
import { LoadingComponent } from "./loading/loading.component";
import { ProfilePictureComponent } from "./profilePicture/profilePicture.component";
import { RectanglePictureComponent } from "./rectangleProfilePicture/rectanglePicture.component";
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
    onClickOutsideHideDirective
  ]
})

export class TemplateModule {}
