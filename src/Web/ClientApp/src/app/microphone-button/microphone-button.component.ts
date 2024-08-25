import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { WinesClient, FileParameter } from '../web-api-client';  // Import the WinesClient and FileParameter
import * as RecordRTC from 'recordrtc';  // Import RecordRTC for recording
import { finalize } from 'rxjs/operators';  // Import finalize operator

@Component({
  selector: 'app-microphone-button',
  templateUrl: './microphone-button.component.html',
  styleUrls: ['./microphone-button.component.css']
})
export class MicrophoneButtonComponent implements OnInit {
  private recordRTC: any;  // RecordRTC object
  private stream: MediaStream | null = null;  // Media stream for recording
  isRecording: boolean = false;  // Flag to control the recording state
  isMicOn: boolean = false;  // State to track if the mic is on or off

  // EventEmitter to notify parent component when a wine is added via voice
  @Output() wineAdded = new EventEmitter<void>();

  constructor(private winesClient: WinesClient) { }  // Inject WinesClient

  ngOnInit(): void {}

  // Method to toggle microphone on/off
  toggleMic() {
    if (this.isMicOn) {
      this.stopRecording();
    } else {
      this.startRecording();
    }
  }

  // Method to start recording
  startRecording() {
    this.isMicOn = true;  // Mic is now on
    this.isRecording = true;

    // Request the user's audio input
    navigator.mediaDevices.getUserMedia({ audio: true }).then((stream) => {
      this.stream = stream;

      // Create a new RecordRTC instance
      this.recordRTC = new RecordRTC(stream, {
        type: 'audio',
        mimeType: 'audio/wav',  // Record as WAV
      });

      // Start recording
      this.recordRTC.startRecording();
    }).catch((err) => {
      console.error('Error accessing microphone', err);
      this.isMicOn = false;  // Reset mic state if there's an error
    });
  }

  // Method to stop recording and send the audio file to the backend
  stopRecording() {
    if (this.isRecording && this.recordRTC) {
      this.isRecording = false;
      this.isMicOn = false;  // Mic is now off

      // Stop recording
      this.recordRTC.stopRecording(() => {
        const audioBlob = this.recordRTC.getBlob();  // Get the audio Blob

        // Create a File object from the Blob
        const audioFile = new File([audioBlob], 'audio.wav', { type: 'audio/wav' });

        // Create FileParameter as expected by the WinesClient
        const fileParameter: FileParameter = {
          data: audioFile,
          fileName: audioFile.name
        };

        // Call the backend API and reload inventory after the request is completed
        this.winesClient.createWineByVoice(fileParameter).pipe(
          finalize(() => {
            // Reload the inventory after the voice wine is successfully added
            console.log('Wine added by voice, reloading inventory.');
            this.wineAdded.emit();  // Emit event to notify parent to reload the wine list
          })
        ).subscribe(
          (response) => {
            console.log('Audio file successfully sent to the server!', response);
          },
          (error) => {
            console.error('Error uploading audio file', error);
          }
        );
      });

      // Stop the media stream
      if (this.stream) {
        this.stream.getTracks().forEach(track => track.stop());
      }
    }
  }
}
