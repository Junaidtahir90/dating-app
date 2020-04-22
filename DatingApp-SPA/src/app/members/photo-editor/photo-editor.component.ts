import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
  @Output() getUserPhotoChange = new EventEmitter<string>();
  userId: number;
  currentMainPhoto: Photo ;
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
        if (photo.isMain) {
          this.authService.changeMemberPhoto(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
      }
    };
  }

 /* getUserId() {
    return this.authService.decodedToken.nameid[0];
  }*/
  setMainPhoto(photo: Photo ) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid[0], photo.id).subscribe(() => {
     // to Change photo icon runtime
      this.currentMainPhoto = this.photos.filter(p => p.isMain)[0];
      this.currentMainPhoto.isMain = false;
      photo.isMain = true;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
      // this.getUserPhotoChange.emit(photo.url);
      // this.alertify.success('Image Set Succesfully');
      console.log('Image Set Succesfully');
    }, error => {
      this.alertify.error(error);
      console.log(error);
    }
    );
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () => {
        this.userService.deletePhoto(this.authService.decodedToken.nameid[0], id).subscribe(() => {
          this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
          this.alertify.success('Photo has been deleted succesfully');
        }, error => {
          this.alertify.error(error);
        });
      });

  }



}
