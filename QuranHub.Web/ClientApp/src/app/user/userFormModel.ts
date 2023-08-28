import { FormControl, FormGroup, Validators } from "@angular/forms";

export class  UserControl extends FormControl{
    label:string;
    modelProperty:string;

    constructor(label:string, property:string, value: any, validator: any){
         super(value, validator);
         this.label = label;
         this.modelProperty = property;
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

export class UserFormModel extends FormGroup {

        constructor(email:string, userName:string) {
            super({
                email: new UserControl("Email", "email", email, Validators.required),

                userName: new UserControl("User Name", "userName", userName,
                    Validators.compose([Validators.required,
                        Validators.pattern("^[A-Za-z0-9_-]{8,30}$")])),

            });

        }
    get userControls(): UserControl[] {
            return Object.keys(this.controls)
                .map(k => this.controls[k] as UserControl);
        }

    getValidationMessages(name: string): string[] {
            return (this.controls['name'] as UserControl).getValidationMessages();
        }
        
    getFormValidationMessages() : string[] {
            let messages: string[] = [];
            Object.values(this.controls).forEach(c => 
                messages.push(...(c as UserControl).getValidationMessages()));
            return messages;
        }
}