import {Component } from "@angular/core";
import { UserService, Gender, IAboutInfo, Religion } from "../abstractions/services/userService";
import { AboutInfoFormModel } from "./aboutInfoFormModel";

@Component({
    selector: 'aboutInfo',
    templateUrl: "aboutInfo.component.html"
})

export class AboutInfoComponent {

    loading: boolean = false;
    aboutInfoForm!: AboutInfoFormModel;
    dataLoaded:boolean = false;
    dateOfBirth!:string;
    gender!: string;
    religion!:string;
    aboutMe!:string;
    message!: string;

    constructor(public userService: UserService) {
        this.getUser();
    }  

    getUser() {
      this.userService.getAboutInfo().subscribe((result:IAboutInfo) => {
            this.dateOfBirth = result.dateOfBirth;
            this.gender = result.gender;
            this.religion = result.religion;
            this.aboutMe = result.aboutMe;
            this.aboutInfoForm = new AboutInfoFormModel(this.dateOfBirth, this.gender, this.religion, this.aboutMe);
            this.dataLoaded = true;
        });
    }
   
    submitForm() {

        if (this.aboutInfoForm.valid){
            this.loading = true;
            this.dateOfBirth = this.aboutInfoForm.controls["dateOfBirth"].value;
            this.gender = this.aboutInfoForm.controls["gender"].value;
            this.religion =this.aboutInfoForm.controls["religion"].value;
            this.aboutMe = this.aboutInfoForm.controls["aboutMe"].value;
            this.userService.editAboutData(this.dateOfBirth, this.gender, this.religion, this.aboutMe).subscribe(result => {

              this.message = "about Information updated";
              this.loading = false;
              this.aboutInfoForm.reset();
              },
                (err) =>{
                  this.message = "an error occured";
                  this.loading = false;
              }
            );
        }
    }
}
