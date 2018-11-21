import { ShareService } from "../share.service";
import { Observable, of } from "rxjs";
import { Session } from "src/app/models/session";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable()
export class ShareServiceRef extends ShareService {

  constructor(private readonly _http: HttpClient) {
    super();

  }
  public shareSession(mail: string, session: Session): Observable<any> {

    return this._http.post(environment.functionUrl, {target: mail, session: session} );
  }

}
