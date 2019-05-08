pipeline {
    agent any
    stages {
        stage('Pull Projects') {
            steps {
                cleanWs()
                dir('AIO') {
                    git 'https://github.com/NeighTools/COM3D2.BepInEx.AIO.git'
                    sh 'git submodule update --init --recursive'
                }
                dir('Common') {
                    git credentialsId: 'b1f2f78b-f0c5-4a81-8b4a-55b6b8bdbbe3', url: 'http://localhost:6000/JenkinsStuff/MiscResources.git'
                }
                dir('Unity') {
                    git credentialsId: 'b1f2f78b-f0c5-4a81-8b4a-55b6b8bdbbe3', url: 'http://localhost:6000/JenkinsStuff/UnityDLL.git'
                }
                dir('Doorstop') { deleteDir() }
                dir('Doorstop') {
                    sh '''  tag="v2.7.1.0";
                    version="2.7.1.0";
                    wget https://github.com/NeighTools/UnityDoorstop/releases/download/$tag/Doorstop_x64_$version.zip;
                    unzip -o Doorstop_x64_$version.zip winhttp.dll -d x64;'''
                }
            }
        }
        stage('Build BepInEx') {
            steps {
                dir('AIO/BepInEx') {
                    sh "mkdir -p lib"

                    // Ghetto fix to force TargetFrameworks to only net35
                    sh "find . -type f -name \"*.csproj\" -exec sed -i -E \"s/(<TargetFrameworks>)[^<]+(<\\/TargetFrameworks>)/\\1net35\\2/g\" {} +"
                    sh "nuget restore"
                }
                sh 'cp -f Unity/5.6/UnityEngine.dll AIO/BepInEx/lib/UnityEngine.dll'
                dir('AIO/BepInEx') {
                    sh 'msbuild /p:Configuration=Legacy /t:Build /p:DebugType=none BepInEx.sln'

                    dir("bin/patcher") {
                        deleteDir()
                    }
                }
            }
        }
        stage('Build UnityInjectorLoader') {
            steps {
                sh 'mkdir -p AIO/BepInEx.UnityInjectorLoader/lib'
                sh 'cp -f Unity/5.6/UnityEngine.dll AIO/BepInEx.UnityInjectorLoader/lib/UnityEngine.dll'
                sh 'cp -f AIO/BepInEx/bin/BepInEx.dll AIO/BepInEx.UnityInjectorLoader/lib/BepInEx.dll'
                sh 'mkdir -p AIO/BepInEx.UnityInjectorLoader/BepInEx.UnityInjectorLoader/bin/Release'
                sh 'cp -f AIO/BepInEx/bin/0Harmony.dll AIO/BepInEx.UnityInjectorLoader/lib/0Harmony.dll'
                dir('AIO/BepInEx.UnityInjectorLoader/BepInEx.UnityInjectorLoader') {
                    sh 'msbuild /p:Configuration=Release /t:Build /p:DebugType=none BepInEx.UnityInjectorLoader.csproj'
                }
            }
        }
        stage('Build SybarisLoader') {
            steps {
                sh 'mkdir -p AIO/BepInEx.SybarisLoader.Patcher/lib'
                sh 'cp -f AIO/BepInEx/bin/BepInEx.dll AIO/BepInEx.SybarisLoader.Patcher/lib/BepInEx.dll'
                dir('AIO/BepInEx.SybarisLoader.Patcher') {
                    sh 'nuget restore'
                }
                dir('AIO/BepInEx.SybarisLoader.Patcher/BepInEx.SybarisLoader.Patcher') {
                    sh 'msbuild /p:Configuration=Release /t:Build /p:DebugType=none BepInEx.SybarisLoader.Patcher.csproj'
                }
            }
        }
        stage('Build SybarisMigrator') {
            steps {
                dir('AIO/SybarisMigrator') {
                    sh 'nuget restore'
                }
                dir('AIO/SybarisMigrator/SybarisMigrator') {
                    sh 'msbuild /p:Configuration=Release /t:Build /p:DebugType=none SybarisMigrator.csproj'
                }
            }
        }
        stage('Package everything') {
            steps {
                dir('AIO/docs') {
                    script {
                        findFiles(glob: '*.md').each {
                            sh "pandoc ${it.path} -f markdown -t html -o ${it.name.substring(0, it.name.length() - 3)}.html --template ../../Common/pandoc/TEMPLATE.html"
                        }    
                    }
                }
                dir('Build') { deleteDir() }
                dir('Build') {
                    sh 'mkdir -p BepInEx/core BepInEx/patchers/SybarisLoader BepInEx/config BepInEx/plugins/UnityInjectorLoader Sybaris/UnityInjector/Config'
                    sh 'cp -f -t BepInEx/core ../AIO/BepInEx/bin/*'
                    
                    sh 'cp -f ../AIO/config/* BepInEx/config'

                    sh 'cp -f ../AIO/BepInEx/doorstop/doorstop_config.ini doorstop_config.ini'
                    sh 'cp -f ../Doorstop/x64/winhttp.dll winhttp.dll'

                    sh 'cp -f ../AIO/docs/*.html .'
                    
                    sh 'cp -f ../AIO/SybarisMigrator/SybarisMigrator/bin/Release/SybarisMigrator.exe SybarisMigrator.exe'
                    
                    sh 'cp -f ../AIO/BepInEx.SybarisLoader.Patcher/BepInEx.SybarisLoader.Patcher/bin/Release/BepInEx.SybarisLoader.Patcher.dll BepInEx/patchers/SybarisLoader/BepInEx.SybarisLoader.Patcher.dll'
                    sh 'cp -f ../AIO/BepInEx.UnityInjectorLoader/BepInEx.UnityInjectorLoader/bin/Release/BepInEx.UnityInjectorLoader.dll BepInEx/plugins/UnityInjectorLoader/BepInEx.UnityInjectorLoader.dll'
                    sh 'cp -f ../AIO/BepInEx.UnityInjectorLoader/BepInEx.UnityInjectorLoader/bin/Release/ExIni.dll BepInEx/plugins/UnityInjectorLoader/ExIni.dll'

                    sh 'zip -r9 "COM3D2.BepInEx_Sybaris_AIO.zip" ./*'
                }
            }
        }
                    stage('Saving artifacts') {
                steps {
                    archiveArtifacts 'Build/COM3D2.BepInEx_Sybaris_AIO.zip'
                }
            }
    }
    post {
        always {
            echo "**Build:** [${currentBuild.id}](${env.BUILD_URL})\n**Status:** [${currentBuild.currentResult}](${env.BUILD_URL})"
            echo "${env.JOB_NAME} #${currentBuild.id}"
            echo "${env.BUILD_URL}artifact/Build/COM3D2.BepInEx_Sybaris_AIO.zip"
            echo "${currentBuild.changeSets}"
            withCredentials([string(credentialsId: 'discord-horse-webhook', variable: 'DISCORD_WEBHOOK')]) {
                discordSend description: "**Build:** [${currentBuild.id}](${env.BUILD_URL})\n**Status:** [${currentBuild.currentResult}](${env.BUILD_URL})\n\n**Artifacts:**\n- [COM3D2.BepInEx_Sybaris_AIO.zip](${env.BUILD_URL}artifact/Build/COM3D2.BepInEx_Sybaris_AIO.zip)", footer: 'Jenkins via Discord Notifier', link: env.BUILD_URL, successful: currentBuild.resultIsBetterOrEqualTo('SUCCESS'), title: "${env.JOB_NAME} #${currentBuild.id}", webhookURL: DISCORD_WEBHOOK
            }
        }
    }
}