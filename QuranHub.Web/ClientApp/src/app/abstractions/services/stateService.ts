import { BehaviorSubject, Observable } from "rxjs";

export abstract class StateService extends BehaviorSubject<any>  {

  abstract editState(state: any): any;

  abstract update(state: any): void;

  abstract storeSessionData<T>(dataType: string, data: T): void;

  abstract getSessionData<T>(dataType: string): Observable<T>;
}
