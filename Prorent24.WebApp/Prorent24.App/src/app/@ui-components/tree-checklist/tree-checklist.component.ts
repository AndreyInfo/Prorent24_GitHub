import { Component, EventEmitter, OnInit, Input,Output, Injectable } from '@angular/core';
import { ITreeOptions } from 'angular-tree-component';

@Component({
  selector: 'tree-checklist',
  templateUrl: 'tree-checklist.component.html',
  styleUrls: ['tree-checklist.component.css']
})
export class TreeChecklistComponent implements OnInit {

  @Input() tableSource: any;
  @Output() checklistCheck: EventEmitter<any> = new EventEmitter<any>();

  showNodes = false;
  nodes: any = [];

  ngOnInit() {
    let data = this.tableSource;
    for (let i = 0; i < data.source.localdata.length; i++) {
      let nodeIndex = this.nodes.findIndex(x => x.name == data.source.localdata[i].folderName)

      if (nodeIndex == -1) {
        let node: any = {};
        node= {
          name: data.source.localdata[i].folderName,
          children:[{
            id: data.source.localdata[i].id,
            name: data.source.localdata[i].name
          }]
        }
        this.nodes.push(node);
      }
      else{
        this.nodes[nodeIndex].children.push({
          id: data.source.localdata[i].id,
          name: data.source.localdata[i].name
        })
      }
    }

    this.showNodes = true;
  }

  onCheck(node:any, $event:any){
    if(!node.isActive){
      node.isActive = true;
    }
    else{
      node.isActive = false;
    }

   this.checklistCheck.emit(this.nodes);
  }

  // nodes = [
  //   {
  //     name: 'root1',
  //     children: [
  //       { name: 'child1' },
  //       { name: 'child2' }
  //     ]
  //   },
  //   {
  //     name: 'root2',
  //     children: [
  //       { name: 'child1' },
  //       { name: 'child2' }
  //     ]
  //   },
  //   {
  //     name: 'root3',
  //     children: [
  //       { name: 'child1' },
  //       { name: 'child2' }
  //     ]
  //   }
  // ];

  options: ITreeOptions = {
    useCheckbox: true,
    getChildren: this.getChildren.bind(this)
  };

  optionsDisabled: ITreeOptions = {
    useCheckbox: true,
    getChildren: this.getChildren.bind(this),
    useTriState: false
  };

  getChildren(node: any) {
    const newNodes = [
      {
        name: 'child1'
      }, {
        name: 'child2'
      }
    ];

    return new Promise((resolve, reject) => {
      setTimeout(() => resolve(newNodes), 1000);
    });
  }

}


/**  Copyright 2018 Google Inc. All Rights Reserved.
    Use of this source code is governed by an MIT-style license that
    can be found in the LICENSE file at http://angular.io/license */