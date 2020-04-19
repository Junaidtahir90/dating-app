import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_service/auth.service';
import { UserService } from 'src/app/_service/user.service';
import { AlertifyService } from 'src/app/_service/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() photos: Photo[];
  userId: number;
  uploader: FileUploader;
  hasBaseDropZoneOver: false;
  baseUrl = environment.apiUrl;
  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.initialzerUploader();
    // this.userId = + this.authService.decodedToken.nameid[0];
  }


  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  initialzerUploader() {
    this.uploader = new FileUploader (
      {
        url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid[0] + '/photos',
        authToken : 'Bearer ' + localStorage.getItem('token'),
        isHTML5 : true,
        allowedFileType : ['image'],
        removeAfterUpload : true,
        autoUpload: false,
        maxFileSize : 10 * 1024 * 1024,

      }
    );
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          description: res.description,
          dateAdded: res.dateAdded,
          isMain: res.isMain,
        };
        this.photos.push(photo);
      }
    };
  }

 /* getUserId() {
    return this.authService.decodedToken.nameid[0];
  }*/
  setMainPhoto(photo: Photo ) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid[0], photo.id).subscribe(() => {
      this.alertify.success('Image Set Succesfully');
      console.log('Image Set Succesfully');
    }, error => {
      this.alertify.error(error);
      console.log(error);
    }
    );
  }

}
