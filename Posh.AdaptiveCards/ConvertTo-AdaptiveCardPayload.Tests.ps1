Import-Module $PSScriptRoot\Posh.AdaptiveCards.dll

Describe "ConvertTo-AdaptiveCradPayload" {
  BeforeAll {
    $sampleTemplate = @'
    {
      "type": "AdaptiveCard",
      "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
      "version": "1.2",
      "body": [
          {
              "type": "TextBlock",
              "text": "Access Control List for '${$root.FolderInfo.BaseName}'",
              "wrap": true,
              "size": "Large",
              "weight": "Bolder"
          },
          {
              "type": "TextBlock",
              "text": "${$root.FolderInfo.FullName}",
              "spacing": "None",
              "isSubtle": true,
              "wrap": false
          },
          {
              "type": "ColumnSet",
              "columns": [
                  {
                      "type": "Column",
                      "width": "stretch",
                      "items": [
                          {
                              "type": "TextBlock",
                              "text": "Attributes",
                              "wrap": true,
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${$root.FolderInfo.Attributes}",
                              "spacing": "None"
                          },
                          {
                              "type": "TextBlock",
                              "text": "Created",
                              "wrap": true,
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${formatDateTime($root.FolderInfo.CreationTime,'MM-dd-yyyy hh:mm:ss tt')}",
                              "wrap": false,
                              "spacing": "None"
                          }
                      ]
                  },
                  {
                      "type": "Column",
                      "width": "stretch",
                      "items": [
                          {
                              "type": "TextBlock",
                              "text": "Owner",
                              "wrap": true,
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${$root.ACL.Owner}",
                              "spacing": "None"
                          },
                          {
                              "type": "TextBlock",
                              "text": "Modified",
                              "wrap": true,
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${formatDateTime($root.FolderInfo.LastWriteTime,'MM-dd-yyyy hh:mm:ss tt')}",
                              "wrap": false,
                              "spacing": "None"
                          }
                      ]
                  },
                  {
                      "type": "Column",
                      "width": "stretch",
                      "items": [
                          {
                              "type": "TextBlock",
                              "wrap": true,
                              "text": "Group",
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${$root.ACL.Group}",
                              "spacing": "None"
                          },
                          {
                              "type": "TextBlock",
                              "text": "Last Accessed",
                              "wrap": true,
                              "weight": "Bolder",
                              "isSubtle": true
                          },
                          {
                              "type": "TextBlock",
                              "text": "${formatDateTime($root.FolderInfo.LastAccessTime,'MM-dd-yyyy hh:mm:ss tt')}",
                              "wrap": false,
                              "spacing": "None"
                          }
                      ]
                  }
              ]
          }
      ],
      "actions": [
          {
              "type": "Action.ShowCard",
              "title": "Show Access Control List",
              "card": {
                  "type": "AdaptiveCard",
                  "body": [
                      {
                          "type": "FactSet",
                          "$data": "${ACL.Access}",
                          "facts": [
                              {
                                  "title": "Identity Reference",
                                  "value": "${IdentityReference.Value}"
                              },
                              {
                                  "title": "File System Rights",
                                  "value": "${FileSystemRights} "
                              },
                              {
                                  "title": "Inheritance Flags",
                                  "value": "${InheritanceFlags}"
                              },
                              {
                                  "title": "Propagation Flags",
                                  "value": "${PropagationFlags}"
                              }
                          ]
                      }
                  ]
              }
          }
      ]
    }
'@
    $sampleData = @'
    {
      "ACL": {
        "Path": "Microsoft.PowerShell.Core\\FileSystem::E:\\Posh.AdaptiveCards\\Posh.AdaptiveCards\\bin\\Debug\\netstandard2.0\\publish",
        "Owner": "COMPUTER\\parithon",
        "Group": "COMPUTER\\Developers",
        "Access": [
          {
            "FileSystemRights": "FullControl",
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "BUILTIN\\Administrators"
            },
            "IsInherited": true,
            "InheritanceFlags": "None",
            "PropagationFlags": "None"
          },
          {
            "FileSystemRights": 268435456,
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "BUILTIN\\Administrators"
            },
            "IsInherited": true,
            "InheritanceFlags": "ContainerInherit, ObjectInherit",
            "PropagationFlags": "InheritOnly"
          },
          {
            "FileSystemRights": "FullControl",
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "NT AUTHORITY\\SYSTEM"
            },
            "IsInherited": true,
            "InheritanceFlags": "None",
            "PropagationFlags": "None"
          },
          {
            "FileSystemRights": 268435456,
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "NT AUTHORITY\\SYSTEM"
            },
            "IsInherited": true,
            "InheritanceFlags": "ContainerInherit, ObjectInherit",
            "PropagationFlags": "InheritOnly"
          },
          {
            "FileSystemRights": "Modify, Synchronize",
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "NT AUTHORITY\\Authenticated Users"
            },
            "IsInherited": true,
            "InheritanceFlags": "None",
            "PropagationFlags": "None"
          },
          {
            "FileSystemRights": -536805376,
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "NT AUTHORITY\\Authenticated Users"
            },
            "IsInherited": true,
            "InheritanceFlags": "ContainerInherit, ObjectInherit",
            "PropagationFlags": "InheritOnly"
          },
          {
            "FileSystemRights": "ReadAndExecute, Synchronize",
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "BUILTIN\\Users"
            },
            "IsInherited": true,
            "InheritanceFlags": "None",
            "PropagationFlags": "None"
          },
          {
            "FileSystemRights": -1610612736,
            "AccessControlType": "Allow",
            "IdentityReference": {
              "Value": "BUILTIN\\Users"
            },
            "IsInherited": true,
            "InheritanceFlags": "ContainerInherit, ObjectInherit",
            "PropagationFlags": "InheritOnly"
          }
        ]
      },
      "FolderInfo": {
        "FullName": "E:\\Posh.AdaptiveCards\\Posh.AdaptiveCards\\bin\\Debug\\netstandard2.0\\publish",
        "CreationTime": "2021-09-12T00:00:00.0000000Z",
        "LastWriteTime": "2021-09-12T00:00:00.0000000Z",
        "LastAccessTime": "2021-09-12T00:00:00.0000000Z",
        "BaseName": "publish",
        "Attributes": "Directory"
      }
    }    
'@
    $expectedPayload = @'
    {"type":"AdaptiveCard","$schema":"http://adaptivecards.io/schemas/adaptive-card.json","version":"1.2","body":[{"type":"TextBlock","text":"Access Control List for 'publish'","wrap":true,"size":"Large","weight":"Bolder"},{"type":"TextBlock","text":"E:\\Posh.AdaptiveCards\\Posh.AdaptiveCards\\bin\\Debug\\netstandard2.0\\publish","spacing":"None","isSubtle":true,"wrap":false},{"type":"ColumnSet","columns":[{"type":"Column","width":"stretch","items":[{"type":"TextBlock","text":"Attributes","wrap":true,"weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"Directory","spacing":"None"},{"type":"TextBlock","text":"Created","wrap":true,"weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"09-12-2021 12:00:00 AM","wrap":false,"spacing":"None"}]},{"type":"Column","width":"stretch","items":[{"type":"TextBlock","text":"Owner","wrap":true,"weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"COMPUTER\\parithon","spacing":"None"},{"type":"TextBlock","text":"Modified","wrap":true,"weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"09-12-2021 12:00:00 AM","wrap":false,"spacing":"None"}]},{"type":"Column","width":"stretch","items":[{"type":"TextBlock","wrap":true,"text":"Group","weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"COMPUTER\\Developers","spacing":"None"},{"type":"TextBlock","text":"Last Accessed","wrap":true,"weight":"Bolder","isSubtle":true},{"type":"TextBlock","text":"09-12-2021 12:00:00 AM","wrap":false,"spacing":"None"}]}]}],"actions":[{"type":"Action.ShowCard","title":"Show Access Control List","card":{"type":"AdaptiveCard","body":[{"type":"FactSet","facts":[{"title":"Identity Reference","value":"BUILTIN\\Administrators"},{"title":"File System Rights","value":"FullControl "},{"title":"Inheritance Flags","value":"None"},{"title":"Propagation Flags","value":"None"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"BUILTIN\\Administrators"},{"title":"File System Rights","value":"268435456 "},{"title":"Inheritance Flags","value":"ContainerInherit, ObjectInherit"},{"title":"Propagation Flags","value":"InheritOnly"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"NT AUTHORITY\\SYSTEM"},{"title":"File System Rights","value":"FullControl "},{"title":"Inheritance Flags","value":"None"},{"title":"Propagation Flags","value":"None"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"NT AUTHORITY\\SYSTEM"},{"title":"File System Rights","value":"268435456 "},{"title":"Inheritance Flags","value":"ContainerInherit, ObjectInherit"},{"title":"Propagation Flags","value":"InheritOnly"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"NT AUTHORITY\\Authenticated Users"},{"title":"File System Rights","value":"Modify, Synchronize "},{"title":"Inheritance Flags","value":"None"},{"title":"Propagation Flags","value":"None"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"NT AUTHORITY\\Authenticated Users"},{"title":"File System Rights","value":"-536805376 "},{"title":"Inheritance Flags","value":"ContainerInherit, ObjectInherit"},{"title":"Propagation Flags","value":"InheritOnly"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"BUILTIN\\Users"},{"title":"File System Rights","value":"ReadAndExecute, Synchronize "},{"title":"Inheritance Flags","value":"None"},{"title":"Propagation Flags","value":"None"}]},{"type":"FactSet","facts":[{"title":"Identity Reference","value":"BUILTIN\\Users"},{"title":"File System Rights","value":"-1610612736 "},{"title":"Inheritance Flags","value":"ContainerInherit, ObjectInherit"},{"title":"Propagation Flags","value":"InheritOnly"}]}]}}]}
'@.Trim()
    $sampleTemplatePath = "TestDrive:\acl.template.json"
    Set-Content $sampleTemplatePath -Value $sampleTemplate
  }
  Context "Template"  {
    It "Should convert the template and data to a payload" {
      $result = ConvertTo-AdaptiveCardPayload -InputObject $sampleData -Template $sampleTemplate
      $result | Should -BeOfType [string]
      $result | Should -BeExactly $expectedPayload
    }
  }
  Context "TemplatePath" {
    It "Should convert the data and template to a payload" {
      $result = ConvertTo-AdaptiveCardPayload -InputObject $sampleData -TemplatePath $sampleTemplatePath
      $result | Should -BeOfType [string]
      $result | Should -BeExactly $expectedPayload
    }
  }
}
