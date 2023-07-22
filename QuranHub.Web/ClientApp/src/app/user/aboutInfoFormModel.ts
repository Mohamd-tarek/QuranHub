import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Gender, Religion } from "../abstractions/services/userService";

export class AboutInfoControl extends FormControl{

    label:string;
    modelProperty:string;
    type: string;
    options:any

    constructor(label:string, property:string, value: any, validator: any,  type: string = "text", options: any = null){
         super(value, validator);
         this.label = label;
         this.modelProperty = property;
         this.type = type;
         this.options = options;
    }

    getValidationMessages() {
        let messages: string[] = [];
        
        if (this.errors) {

            for (let errorName in this.errors) {
                switch (errorName) {
                    case "required":
                        messages.push(`You must enter a ${this.label}`);
                        break;
                    case "minlength":
                        messages.push(`A ${this.label} must be at least
                            ${this.errors['minlength'].requiredLength}
                            characters`);
                        break;
                    case "maxlength":
                        messages.push(`A ${this.label} must be no more than
                            ${this.errors['maxlength'].requiredLength}
                            characters`);
                        break;
                    case "pattern":
                        messages.push(`The ${this.label} contains
                             illegal characters`);
                        break;
                    case "limit":
                        messages.push(`A ${this.label} cannot be more
                            than ${this.errors['limit'].limit}`);
                        break;    
                }
            }
        }
        return messages;
    }
}

export class AboutInfoFormModel extends FormGroup {
  
        constructor(dateOfBirth:string, gender:string, religion:string, aboutMe: string) {
            super({
                dateOfBirth: new AboutInfoControl("Date of birth", "dateOfBirth", dateOfBirth, Validators.required, "date" ),
                gender: new AboutInfoControl("Gender", "gender", gender, Validators.required,  "select", Gender),
                religion: new AboutInfoControl("Religion", "religion", religion, Validators.required, "select", Religion),
                aboutMe: new AboutInfoControl("About Me", "aboutMe", aboutMe, Validators.required),
            });

        }
    get aboutInfoControls(): AboutInfoControl[] {
            return Object.keys(this.controls)
                .map(k => this.controls[k] as AboutInfoControl);
        }

    getValidationMessages(name: string): string[] {
            return (this.controls['name'] as AboutInfoControl).getValidationMessages();
        }
        
    getFormValidationMessages() : string[] {
            let messages: string[] = [];
            Object.values(this.controls).forEach(c => 
                messages.push(...(c as AboutInfoControl).getValidationMessages()));
            return messages;
        }
}
