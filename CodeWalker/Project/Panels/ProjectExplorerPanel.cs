﻿using CodeWalker.GameFiles;
using CodeWalker.World;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeWalker.Project.Panels
{
    public partial class ProjectExplorerPanel : ProjectPanel
    {
        public ProjectForm ProjectForm { get; set; }
        public ProjectFile CurrentProjectFile { get; set; }

        private bool inDoubleClick = false; //used in disabling double-click to expand tree nodes
        private List<TreeNode> SelectedNodes = new List<TreeNode>();

        public ProjectExplorerPanel(ProjectForm projectForm)
        {
            ProjectForm = projectForm;
            InitializeComponent();
        }


        public void LoadProjectTree(ProjectFile projectFile)
        {
            ProjectTreeView.Nodes.Clear();

            CurrentProjectFile = projectFile;
            if (CurrentProjectFile == null) return;

            var pcstr = CurrentProjectFile.HasChanged ? "*" : "";

            var projnode = ProjectTreeView.Nodes.Add(pcstr + CurrentProjectFile.Name);
            projnode.Tag = CurrentProjectFile;


            if (CurrentProjectFile.YmapFiles.Count > 0)
            {
                var ymapsnode = projnode.Nodes.Add("Ymap 文件");
                ymapsnode.Name = "Ymap";

                foreach (var ymapfile in CurrentProjectFile.YmapFiles)
                {
                    var ycstr = ymapfile.HasChanged ? "*" : "";
                    string name = ymapfile.Name;
                    if (ymapfile.RpfFileEntry != null)
                    {
                        name = ymapfile.RpfFileEntry.Name;
                    }
                    var ymapnode = ymapsnode.Nodes.Add(ycstr + name);
                    ymapnode.Tag = ymapfile;

                    LoadYmapTreeNodes(ymapfile, ymapnode);

                    JenkIndex.Ensure(name);
                    JenkIndex.Ensure(Path.GetFileNameWithoutExtension(name));
                }
                ymapsnode.Expand();
            }

            if (CurrentProjectFile.YtypFiles.Count > 0)
            {
                var ytypsnode = projnode.Nodes.Add("Ytyp 文件");
                ytypsnode.Name = "Ytyp";

                foreach (var ytypfile in CurrentProjectFile.YtypFiles)
                {
                    var ycstr = ytypfile.HasChanged ? "*" : "";
                    string name = ytypfile.Name;
                    if (ytypfile.RpfFileEntry != null)
                    {
                        name = ytypfile.RpfFileEntry.Name;
                    }
                    var ytypnode = ytypsnode.Nodes.Add(ycstr + name);
                    ytypnode.Tag = ytypfile;

                    LoadYtypTreeNodes(ytypfile, ytypnode);

                    JenkIndex.Ensure(name);
                    JenkIndex.Ensure(Path.GetFileNameWithoutExtension(name));
                }
                ytypsnode.Expand();
            }

            if (CurrentProjectFile.YbnFiles.Count > 0)
            {
                var ybnsnode = projnode.Nodes.Add("Ybn 文件");
                ybnsnode.Name = "Ybn";

                foreach (var ybnfile in CurrentProjectFile.YbnFiles)
                {
                    var ycstr = ybnfile.HasChanged ? "*" : "";
                    string name = ybnfile.Name;
                    if (ybnfile.RpfFileEntry != null)
                    {
                        name = ybnfile.RpfFileEntry.Name;
                    }
                    var yndnode = ybnsnode.Nodes.Add(ycstr + name);
                    yndnode.Tag = ybnfile;

                    LoadYbnTreeNodes(ybnfile, yndnode);
                }
                ybnsnode.Expand();
            }

            if (CurrentProjectFile.YndFiles.Count > 0)
            {
                var yndsnode = projnode.Nodes.Add("Ynd 文件");
                yndsnode.Name = "Ynd";

                foreach (var yndfile in CurrentProjectFile.YndFiles)
                {
                    var ycstr = yndfile.HasChanged ? "*" : "";
                    string name = yndfile.Name;
                    if (yndfile.RpfFileEntry != null)
                    {
                        name = yndfile.RpfFileEntry.Name;
                    }
                    var yndnode = yndsnode.Nodes.Add(ycstr + name);
                    yndnode.Tag = yndfile;

                    LoadYndTreeNodes(yndfile, yndnode);
                }
                yndsnode.Expand();
            }

            if (CurrentProjectFile.YnvFiles.Count > 0)
            {
                var ynvsnode = projnode.Nodes.Add("Ynv 文件");
                ynvsnode.Name = "Ynv";

                foreach (var ynvfile in CurrentProjectFile.YnvFiles)
                {
                    var ycstr = ynvfile.HasChanged ? "*" : "";
                    string name = ynvfile.Name;
                    if (ynvfile.RpfFileEntry != null)
                    {
                        name = ynvfile.RpfFileEntry.Name;
                    }
                    var ynvnode = ynvsnode.Nodes.Add(ycstr + name);
                    ynvnode.Tag = ynvfile;

                    LoadYnvTreeNodes(ynvfile, ynvnode);
                }
                ynvsnode.Expand();
            }

            if (CurrentProjectFile.TrainsFiles.Count > 0)
            {
                var trainsnode = projnode.Nodes.Add("火车轨道文件");
                trainsnode.Name = "Trains";

                foreach (var trainfile in CurrentProjectFile.TrainsFiles)
                {
                    var tcstr = trainfile.HasChanged ? "*" : "";
                    string name = trainfile.Name;
                    if (trainfile.RpfFileEntry != null)
                    {
                        name = trainfile.RpfFileEntry.Name;
                    }
                    var trainnode = trainsnode.Nodes.Add(tcstr + name);
                    trainnode.Tag = trainfile;

                    LoadTrainTrackTreeNodes(trainfile, trainnode);
                }
                trainsnode.Expand();
            }

            if (CurrentProjectFile.ScenarioFiles.Count > 0)
            {
                var scenariosnode = projnode.Nodes.Add("场景文件");
                scenariosnode.Name = "Scenarios";

                foreach (var scenariofile in CurrentProjectFile.ScenarioFiles)
                {
                    var scstr = scenariofile.HasChanged ? "*" : "";
                    string name = scenariofile.Name;
                    if (scenariofile.RpfFileEntry != null)
                    {
                        name = scenariofile.RpfFileEntry.Name;
                    }
                    var scenarionode = scenariosnode.Nodes.Add(scstr + name);
                    scenarionode.Tag = scenariofile;

                    LoadScenarioTreeNodes(scenariofile, scenarionode);
                }
                scenariosnode.Expand();
            }

            if (CurrentProjectFile.AudioRelFiles.Count > 0)
            {
                var audiorelsnode = projnode.Nodes.Add("音频 Rel 文件");
                audiorelsnode.Name = "AudioRels";

                foreach (var audiorelfile in CurrentProjectFile.AudioRelFiles)
                {
                    var acstr = audiorelfile.HasChanged ? "*" : "";
                    string name = audiorelfile.Name;
                    if (audiorelfile.RpfFileEntry != null)
                    {
                        name = audiorelfile.RpfFileEntry.Name;
                    }
                    var audiorelnode = audiorelsnode.Nodes.Add(acstr + name);
                    audiorelnode.Tag = audiorelfile;

                    LoadAudioRelTreeNodes(audiorelfile, audiorelnode);
                }
                audiorelsnode.Expand();
            }

            if (CurrentProjectFile.YdrFiles.Count > 0)
            {
                var ydrsnode = projnode.Nodes.Add("Ydr 文件");
                ydrsnode.Name = "Ydr";

                foreach (var ydrfile in CurrentProjectFile.YdrFiles)
                {
                    var ycstr = "";// ydrfile.HasChanged ? "*" : "";
                    string name = ydrfile.Name;
                    if (ydrfile.RpfFileEntry != null)
                    {
                        name = ydrfile.RpfFileEntry.Name;
                    }
                    var ydrnode = ydrsnode.Nodes.Add(ycstr + name);
                    ydrnode.Tag = ydrfile;

                    //LoadYdrTreeNodes(ydrfile, ydrnode);
                }
                ydrsnode.Expand();
            }

            if (CurrentProjectFile.YddFiles.Count > 0)
            {
                var yddsnode = projnode.Nodes.Add("Ydd 文件");
                yddsnode.Name = "Ydd";

                foreach (var yddfile in CurrentProjectFile.YddFiles)
                {
                    var ycstr = "";// yddfile.HasChanged ? "*" : "";
                    string name = yddfile.Name;
                    if (yddfile.RpfFileEntry != null)
                    {
                        name = yddfile.RpfFileEntry.Name;
                    }
                    var yddnode = yddsnode.Nodes.Add(ycstr + name);
                    yddnode.Tag = yddfile;

                    //LoadYddTreeNodes(yddfile, yddnode);
                }
                yddsnode.Expand();
            }

            if (CurrentProjectFile.YftFiles.Count > 0)
            {
                var yftsnode = projnode.Nodes.Add("Yft 文件");
                yftsnode.Name = "Yft";

                foreach (var yftfile in CurrentProjectFile.YftFiles)
                {
                    var ycstr = "";// yftfile.HasChanged ? "*" : "";
                    string name = yftfile.Name;
                    if (yftfile.RpfFileEntry != null)
                    {
                        name = yftfile.RpfFileEntry.Name;
                    }
                    var yftnode = yftsnode.Nodes.Add(ycstr + name);
                    yftnode.Tag = yftfile;

                    //LoadYftTreeNodes(yftfile, yftnode);
                }
                yftsnode.Expand();
            }

            if (CurrentProjectFile.YtdFiles.Count > 0)
            {
                var ytdsnode = projnode.Nodes.Add("Ytd 文件");
                ytdsnode.Name = "Ytd";

                foreach (var ytdfile in CurrentProjectFile.YtdFiles)
                {
                    var ycstr = "";// ytdfile.HasChanged ? "*" : "";
                    string name = ytdfile.Name;
                    if (ytdfile.RpfFileEntry != null)
                    {
                        name = ytdfile.RpfFileEntry.Name;
                    }
                    var ytdnode = ytdsnode.Nodes.Add(ycstr + name);
                    ytdnode.Tag = ytdfile;

                    //LoadYtdTreeNodes(ytdfile, ytdnode);
                }
                ytdsnode.Expand();
            }

            // Debug Log
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: LoadProjectTree: " + CurrentProjectFile.Name);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YmapFiles.Count: " + CurrentProjectFile.YmapFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YtypFiles.Count: " + CurrentProjectFile.YtypFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YbnFiles.Count: " + CurrentProjectFile.YbnFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YndFiles.Count: " + CurrentProjectFile.YndFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YnvFiles.Count: " + CurrentProjectFile.YnvFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.TrainsFiles.Count: " + CurrentProjectFile.TrainsFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.ScenarioFiles.Count: " + CurrentProjectFile.ScenarioFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.AudioRelFiles.Count: " + CurrentProjectFile.AudioRelFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YdrFiles.Count: " + CurrentProjectFile.YdrFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YddFiles.Count: " + CurrentProjectFile.YddFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YftFiles.Count: " + CurrentProjectFile.YftFiles.Count);
            System.Diagnostics.Debug.WriteLine("ProjectExplorerPanel: CurrentProjectFile.YtdFiles.Count: " + CurrentProjectFile.YtdFiles.Count);

            projnode.Expand();

        }

        private void LoadYmapTreeNodes(YmapFile ymap, TreeNode node)
        {
            if (ymap == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Entities and CarGens

            node.Nodes.Clear();

            if ((ymap.AllEntities != null) && (ymap.AllEntities.Length > 0))
            {
                var entsnode = node.Nodes.Add("实体 (" + ymap.AllEntities.Length.ToString() + ")");
                entsnode.Name = "Entities";
                entsnode.Tag = ymap;
                var ents = ymap.AllEntities;
                for (int i = 0; i < ents.Length; i++)
                {
                    var ent = ents[i];
                    var edef = ent.CEntityDef;
                    TreeNode enode;
                    if (ProjectForm.displayentityindexes)
                        enode = entsnode.Nodes.Add($"[{i}] {edef.archetypeName}");
                    else
                        enode = entsnode.Nodes.Add(edef.archetypeName.ToString());

                    enode.Tag = ent;
                }
            }
            if ((ymap.CarGenerators != null) && (ymap.CarGenerators.Length > 0))
            {
                var cargensnode = node.Nodes.Add("车辆生成器 (" + ymap.CarGenerators.Length.ToString() + ")");
                cargensnode.Name = "CarGens";
                cargensnode.Tag = ymap;
                var cargens = ymap.CarGenerators;
                for (int i = 0; i < cargens.Length; i++)
                {
                    var cargen = cargens[i];
                    var ccgnode = cargensnode.Nodes.Add(cargen.ToString());
                    ccgnode.Tag = cargen;
                }
            }
            if ((ymap.LODLights?.LodLights != null) && (ymap.LODLights.LodLights.Length > 0))
            {
                var lodlightsnode = node.Nodes.Add("远景灯光 (" + ymap.LODLights.LodLights.Length.ToString() + ")");
                lodlightsnode.Name = "LodLights";
                lodlightsnode.Tag = ymap;
                var lodlights = ymap.LODLights.LodLights;
                for (int i = 0; i < lodlights.Length; i++)
                {
                    var lodlight = lodlights[i];
                    var llnode = lodlightsnode.Nodes.Add(lodlight.ToString());
                    llnode.Tag = lodlight;
                }
            }
            if ((ymap.BoxOccluders != null) && (ymap.BoxOccluders.Length > 0))
            {
                var boxocclsnode = node.Nodes.Add("遮挡盒 (" + ymap.BoxOccluders.Length.ToString() + ")");
                boxocclsnode.Name = "BoxOccluders";
                boxocclsnode.Tag = ymap;
                var boxes = ymap.BoxOccluders;
                for (int i = 0; i < boxes.Length; i++)
                {
                    var box = boxes[i];
                    var boxnode = boxocclsnode.Nodes.Add(box.ToString());
                    boxnode.Tag = box;
                }
            }
            if ((ymap.OccludeModels != null) && (ymap.OccludeModels.Length > 0))
            {
                var occlmodsnode = node.Nodes.Add("遮挡模型 (" + ymap.OccludeModels.Length.ToString() + ")");
                occlmodsnode.Name = "OccludeModels";
                occlmodsnode.Tag = ymap;
                var models = ymap.OccludeModels;
                for (int i = 0; i < models.Length; i++)
                {
                    var model = models[i];
                    var modnode = occlmodsnode.Nodes.Add(model.ToString());
                    modnode.Tag = model;
                }
            }
            if ((ymap.GrassInstanceBatches != null) && (ymap.GrassInstanceBatches.Length > 0))
            {
                var grassbatchesnodes = node.Nodes.Add("批处理实例草 (" + ymap.GrassInstanceBatches.Length.ToString() + ")");
                grassbatchesnodes.Name = "GrassBatches";
                grassbatchesnodes.Tag = ymap;
                var grassbatches = ymap.GrassInstanceBatches;
                for (int i = 0; i < grassbatches.Length; i++)
                {
                    var batch = grassbatches[i];
                    var gbnode = grassbatchesnodes.Nodes.Add(batch.ToString());
                    gbnode.Tag = batch;
                }
            }

        }
        private void LoadYtypTreeNodes(YtypFile ytyp, TreeNode node)
        {
            if (ytyp == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return;

            node.Nodes.Clear();

            if ((ytyp.AllArchetypes != null) && (ytyp.AllArchetypes.Length > 0))
            {
                var archetypesnode = node.Nodes.Add("定义类型 (" + ytyp.AllArchetypes.Length.ToString() + ")");
                archetypesnode.Name = "Archetypes";
                archetypesnode.Tag = ytyp;
                var archetypes = ytyp.AllArchetypes;
                for (int i = 0; i < archetypes.Length; i++)
                {
                    var yarch = archetypes[i];
                    var tarch = archetypesnode.Nodes.Add(yarch.Name);
                    tarch.Tag = yarch;

                    if (yarch is MloArchetype mlo)
                    {
                        var rooms = mlo.rooms;
                        var entities = mlo.entities;
                        var entsets = mlo.entitySets;
                        var portals = mlo.portals;
                        if ((rooms != null) && (rooms.Length > 0))
                        {
                            var roomsnode = tarch.Nodes.Add("房间 (" + rooms.Length.ToString() + ")");
                            roomsnode.Name = "Rooms";
                            for (int j = 0; j < rooms.Length; j++)
                            {
                                var room = rooms[j];
                                var roomnode = roomsnode.Nodes.Add(room.Index.ToString() + ": " + room.RoomName);
                                roomnode.Tag = room;
                                var roomentities = room.AttachedObjects;
                                if ((roomentities != null) && (entities != null))
                                {
                                    for (int k = 0; k < roomentities.Length; k++)
                                    {
                                        var attachedObject = roomentities[k];
                                        if (attachedObject < entities.Length)
                                        {
                                            var ent = entities[attachedObject];
                                            var entnode = roomnode.Nodes.Add(ent.ToString());
                                            entnode.Tag = ent;
                                        }
                                    }
                                }
                            }
                        }
                        if ((portals != null) && (portals.Length > 0))
                        {
                            var portalsnode = tarch.Nodes.Add("门户 (" + portals.Length.ToString() + ")");
                            portalsnode.Name = "Portals";
                            for (int j = 0; j < portals.Length; j++)
                            {
                                var portal = portals[j];
                                var portalnode = portalsnode.Nodes.Add(portal.Name);
                                portalnode.Tag = portal;
                                var portalentities = portal.AttachedObjects;
                                if ((portalentities != null) && (entities != null))
                                {
                                    for (int k = 0; k < portalentities.Length; k++)
                                    {
                                        var attachedObject = portalentities[k];
                                        if (attachedObject < entities.Length)
                                        {
                                            var ent = entities[attachedObject];
                                            var entnode = portalnode.Nodes.Add(ent.ToString());
                                            entnode.Tag = ent;
                                        }
                                    }
                                }
                            }
                        }
                        if ((entsets != null) && (entsets.Length > 0))
                        {
                            var setsnode = tarch.Nodes.Add("实体预设 (" + entsets.Length.ToString() + ")");
                            setsnode.Name = "EntitySets";
                            for (int j = 0; j < entsets.Length; j++)
                            {
                                var entset = entsets[j];
                                var setnode = setsnode.Nodes.Add(entset.Name);
                                setnode.Tag = entset;
                                var setlocs = entset.Locations;
                                var setents = entset.Entities;
                                if ((setents != null) && (setlocs != null))
                                {
                                    for (int k = 0; k < setents.Length; k++)
                                    {
                                        //var loc = (k < setlocs.Length) ? setlocs[k] : 0;
                                        //var room = ((rooms != null) && (loc < rooms.Length)) ? rooms[loc] : null;
                                        //var roomname = (room != null) ? room.RoomName : "[Room not found!]";
                                        var ent = setents[k];
                                        var entnode = setnode.Nodes.Add(/*roomname + ": " + */ ent.ToString());
                                        entnode.Tag = ent;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void LoadYbnTreeNodes(YbnFile ybn, TreeNode node)
        {
            if (ybn == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Nodes

            node.Nodes.Clear();

            if (ybn.Bounds != null)
            {
                LoadYbnBoundsTreeNode(ybn.Bounds, node);
            }

        }
        private void LoadYbnBoundsTreeNode(Bounds b, TreeNode node)
        {

            var boundsnode = node.Nodes.Add(b.Type.ToString());
            boundsnode.Tag = b;

            if (b is BoundComposite bc)
            {
                var children = bc.Children?.data_items;
                if (children != null)
                {
                    for (int i = 0; i < children.Length; i++)
                    {
                        var child = children[i];
                        if (child != null)
                        {
                            LoadYbnBoundsTreeNode(child, boundsnode);
                        }
                    }
                }
            }
            else if (b is BoundGeometry bg)
            {
                TreeNode n;
                n = boundsnode.Nodes.Add("编辑多边形");
                n.Name = "EditPoly";
                n.Tag = b; //this tag should get updated with the selected poly!

                n = boundsnode.Nodes.Add("编辑顶点");
                n.Name = "EditVertex";
                n.Tag = b; //this tag should get updated with the selected vertex!
            }

        }
        private void LoadYndTreeNodes(YndFile ynd, TreeNode node)
        {
            if (ynd == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Nodes

            node.Nodes.Clear();



            if ((ynd.Nodes != null) && (ynd.Nodes.Length > 0))
            {
                var nodesnode = node.Nodes.Add("节点 (" + ynd.Nodes.Length.ToString() + ")");
                nodesnode.Name = "Nodes";
                nodesnode.Tag = ynd;
                var nodes = ynd.Nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    var ynode = nodes[i];
                    var nnode = ynode.RawData;
                    var tnode = nodesnode.Nodes.Add(nnode.ToString());
                    tnode.Tag = ynode;
                }
            }

        }
        private void LoadYnvTreeNodes(YnvFile ynv, TreeNode node)//TODO!
        {
            if (ynv == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Polygons

            node.Nodes.Clear();


            TreeNode n;
            n = node.Nodes.Add("编辑多边形");
            n.Name = "EditPoly";
            n.Tag = ynv; //this tag should get updated with the selected poly!

            n = node.Nodes.Add("编辑门户");
            n.Name = "EditPortal";
            n.Tag = ynv; //this tag should get updated with the selected portal!

            n = node.Nodes.Add("编辑点");
            n.Name = "EditPoint";
            n.Tag = ynv; //this tag should get updated with the selected point!


        }
        private void LoadTrainTrackTreeNodes(TrainTrack track, TreeNode node)
        {
            if (track == null) return;

            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Nodes

            node.Nodes.Clear();



            if ((track.Nodes != null) && (track.Nodes.Count > 0))
            {
                var nodesnode = node.Nodes.Add("节点 (" + track.Nodes.Count.ToString() + ")");
                nodesnode.Name = "Nodes";
                nodesnode.Tag = track;
                var nodes = track.Nodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    var ynode = nodes[i];
                    var tnode = nodesnode.Nodes.Add(ynode.ToString());
                    tnode.Tag = ynode;
                }
            }

        }
        private void LoadScenarioTreeNodes(YmtFile ymt, TreeNode node)
        {
            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Points

            node.Nodes.Clear();

            var region = ymt?.ScenarioRegion;

            if (region == null) return;

            var nodes = region.Nodes;
            if ((nodes == null) || (nodes.Count == 0)) return;

            var pointsnode = node.Nodes.Add("点 (" + nodes.Count.ToString() + ")");
            pointsnode.Name = "Points";
            pointsnode.Tag = ymt;
            for (int i = 0; i < nodes.Count; i++)
            {
                var snode = nodes[i];
                var tnode = pointsnode.Nodes.Add(snode.MedTypeName + ": " + snode.StringText);
                tnode.Tag = snode;
            }

            //var sr = region.Region;
            //if (sr == null) return;
            //int pointCount = (sr.Points?.LoadSavePoints?.Length ?? 0) + (sr.Points?.MyPoints?.Length ?? 0);
            //int entityOverrideCount = (sr.EntityOverrides?.Length ?? 0);
            //int chainCount = (sr.Paths?.Chains?.Length ?? 0);
            //int clusterCount = (sr.Clusters?.Length ?? 0);
            //TreeNode pointsNode = null;
            //TreeNode entityOverridesNode = null;
            //TreeNode chainsNode = null;
            //TreeNode clustersNode = null;
            //if (pointCount > 0)
            //{
            //    pointsNode = node.Nodes.Add("Points (" + pointCount.ToString() + ")");
            //}
            //if (entityOverrideCount > 0)
            //{
            //    entityOverridesNode = node.Nodes.Add("Entity Overrides (" + entityOverrideCount.ToString() + ")");
            //}
            //if (chainCount > 0)
            //{
            //    chainsNode = node.Nodes.Add("Chains (" + chainsNode.ToString() + ")");
            //}
            //if (clusterCount > 0)
            //{
            //    clustersNode = node.Nodes.Add("Clusters (" + clusterCount.ToString() + ")");
            //}
            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    var snode = nodes[i];
            //    if (snode == null) continue;
            //    if ((pointsNode != null) && ((snode.LoadSavePoint != null) || (snode.MyPoint != null)))
            //    {
            //        pointsNode.Nodes.Add(snode.ToString()).Tag = snode;
            //    }
            //    if ((entityOverridesNode != null) && ((snode.EntityOverride != null) || (snode.EntityPoint != null)))
            //    {
            //        entityOverridesNode.Nodes.Add(snode.ToString()).Tag = snode;
            //    }
            //    if ((chainsNode != null) && (snode.ChainingNode != null))
            //    {
            //        chainsNode.Nodes.Add(snode.ToString()).Tag = snode;
            //    }
            //    if ((clustersNode != null) && ((snode.Cluster != null) || (snode.ClusterLoadSavePoint != null) || (snode.ClusterMyPoint != null)))
            //    {
            //        clustersNode.Nodes.Add(snode.ToString()).Tag = snode;
            //    }
            //}

        }
        private void LoadAudioRelTreeNodes(RelFile rel, TreeNode node)
        {
            if (!string.IsNullOrEmpty(node.Name)) return; //named nodes are eg Zones, Emitters

            node.Nodes.Clear();


            if (rel.RelDatasSorted == null) return; //nothing to see here


            var zones = new List<Dat151AmbientZone>();
            var emitters = new List<Dat151AmbientRule>();
            var zonelists = new List<Dat151AmbientZoneList>();
            var emitterlists = new List<Dat151StaticEmitterList>();
            var interiors = new List<Dat151Interior>();
            var interiorrooms = new List<Dat151InteriorRoom>();

            foreach (var reldata in rel.RelDatasSorted)
            {
                if (reldata is Dat151AmbientZone)
                {
                    zones.Add(reldata as Dat151AmbientZone);
                }
                if (reldata is Dat151AmbientRule)
                {
                    emitters.Add(reldata as Dat151AmbientRule);
                }
                if (reldata is Dat151AmbientZoneList)
                {
                    zonelists.Add(reldata as Dat151AmbientZoneList);
                }
                if (reldata is Dat151StaticEmitterList)
                {
                    emitterlists.Add(reldata as Dat151StaticEmitterList);
                }
                if (reldata is Dat151Interior)
                {
                    interiors.Add(reldata as Dat151Interior);
                }
                if (reldata is Dat151InteriorRoom)
                {
                    interiorrooms.Add(reldata as Dat151InteriorRoom);
                }
            }



            if (zones.Count > 0)
            {
                var n = node.Nodes.Add("环境音区域 (" + zones.Count.ToString() + ")");
                n.Name = "AmbientZones";
                n.Tag = rel;

                for (int i = 0; i < zones.Count; i++)
                {
                    var zone = zones[i];
                    var tnode = n.Nodes.Add(zone.NameHash.ToString());
                    tnode.Tag = zone;
                }
            }


            if (emitters.Count > 0)
            {
                var n = node.Nodes.Add("环境音发生器 (" + emitters.Count.ToString() + ")");
                n.Name = "AmbientEmitters";
                n.Tag = rel;

                for (int i = 0; i < emitters.Count; i++)
                {
                    var emitter = emitters[i];
                    var tnode = n.Nodes.Add(emitter.NameHash.ToString());
                    tnode.Tag = emitter;
                }
            }



            if (zonelists.Count > 0)
            {
                var zonelistsnode = node.Nodes.Add("环境音区域列表 (" + zonelists.Count.ToString() + ")");
                zonelistsnode.Name = "AmbientZoneLists";
                zonelistsnode.Tag = rel;
                for (int i = 0; i < zonelists.Count; i++)
                {
                    var zonelist = zonelists[i];
                    var tnode = zonelistsnode.Nodes.Add(zonelist.NameHash.ToString());
                    tnode.Tag = zonelist;
                }
            }

            if (emitterlists.Count > 0)
            {
                var emitterlistsnode = node.Nodes.Add("环境音发生器列表 (" + emitterlists.Count.ToString() + ")");
                emitterlistsnode.Name = "AmbientEmitterLists";
                emitterlistsnode.Tag = rel;
                for (int i = 0; i < emitterlists.Count; i++)
                {
                    var emitterlist = emitterlists[i];
                    var tnode = emitterlistsnode.Nodes.Add(emitterlist.NameHash.ToString());
                    tnode.Tag = emitterlist;
                }
            }

            if (interiors.Count > 0)
            {
                var n = node.Nodes.Add("室内 (" + interiors.Count.ToString() + ")");
                n.Name = "Interiors";
                n.Tag = rel;
                for (int i = 0; i < interiors.Count; i++)
                {
                    var interior = interiors[i];
                    var tnode = n.Nodes.Add(interior.NameHash.ToString());
                    tnode.Tag = interior;
                }
            }

            if (interiorrooms.Count > 0)
            {
                var n = node.Nodes.Add("室内房间 (" + interiorrooms.Count.ToString() + ")");
                n.Name = "InteriorRooms";
                n.Tag = rel;
                for (int i = 0; i < interiorrooms.Count; i++)
                {
                    var room = interiorrooms[i];
                    var tnode = n.Nodes.Add(room.NameHash.ToString());
                    tnode.Tag = room;
                }
            }


        }



        public void SetProjectHasChanged(bool changed)
        {
            if ((ProjectTreeView.Nodes.Count > 0) && (CurrentProjectFile != null))
            {
                //first node is the project...
                string changestr = changed ? "*" : "";
                ProjectTreeView.Nodes[0].Text = changestr + CurrentProjectFile.Name;
            }
        }
        public void SetYmapHasChanged(YmapFile ymap, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var ymnode = GetChildTreeNode(pnode, "Ymap");
                if (ymnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < ymnode.Nodes.Count; i++)
                {
                    var ynode = ymnode.Nodes[i];
                    if (ynode.Tag == ymap)
                    {
                        string name = ymap.Name;
                        if (ymap.RpfFileEntry != null)
                        {
                            name = ymap.RpfFileEntry.Name;
                        }
                        ynode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetYtypHasChanged(YtypFile ytyp, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var ytnode = GetChildTreeNode(pnode, "Ytyp");
                if (ytnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < ytnode.Nodes.Count; i++)
                {
                    var ynode = ytnode.Nodes[i];
                    if (ynode.Tag == ytyp)
                    {
                        string name = ytyp.Name;
                        if (ytyp.RpfFileEntry != null)
                        {
                            name = ytyp.RpfFileEntry.Name;
                        }
                        ynode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetYbnHasChanged(YbnFile ybn, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var ynnode = GetChildTreeNode(pnode, "Ybn");
                if (ynnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < ynnode.Nodes.Count; i++)
                {
                    var ynode = ynnode.Nodes[i];
                    if (ynode.Tag == ybn)
                    {
                        string name = ybn.Name;
                        if (ybn.RpfFileEntry != null)
                        {
                            name = ybn.RpfFileEntry.Name;
                        }
                        ynode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetYndHasChanged(YndFile ynd, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var ynnode = GetChildTreeNode(pnode, "Ynd");
                if (ynnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < ynnode.Nodes.Count; i++)
                {
                    var ynode = ynnode.Nodes[i];
                    if (ynode.Tag == ynd)
                    {
                        string name = ynd.Name;
                        if (ynd.RpfFileEntry != null)
                        {
                            name = ynd.RpfFileEntry.Name;
                        }
                        ynode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetYnvHasChanged(YnvFile ynv, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var ynnode = GetChildTreeNode(pnode, "Ynv");
                if (ynnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < ynnode.Nodes.Count; i++)
                {
                    var ynode = ynnode.Nodes[i];
                    if (ynode.Tag == ynv)
                    {
                        string name = ynv.Name;
                        if (ynv.RpfFileEntry != null)
                        {
                            name = ynv.RpfFileEntry.Name;
                        }
                        ynode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetTrainTrackHasChanged(TrainTrack track, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var trnode = GetChildTreeNode(pnode, "Trains");
                if (trnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < trnode.Nodes.Count; i++)
                {
                    var tnode = trnode.Nodes[i];
                    if (tnode.Tag == track)
                    {
                        string name = track.Name;
                        if (track.RpfFileEntry != null)
                        {
                            name = track.RpfFileEntry.Name;
                        }
                        tnode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetScenarioHasChanged(YmtFile scenario, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var scnode = GetChildTreeNode(pnode, "Scenarios");
                if (scnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < scnode.Nodes.Count; i++)
                {
                    var snode = scnode.Nodes[i];
                    if (snode.Tag == scenario)
                    {
                        string name = scenario.Name;
                        if (scenario.RpfFileEntry != null)
                        {
                            name = scenario.RpfFileEntry.Name;
                        }
                        snode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetAudioRelHasChanged(RelFile rel, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var pnode = ProjectTreeView.Nodes[0];
                var acnode = GetChildTreeNode(pnode, "AudioRels");
                if (acnode == null) return;
                string changestr = changed ? "*" : "";
                for (int i = 0; i < acnode.Nodes.Count; i++)
                {
                    var anode = acnode.Nodes[i];
                    if (anode.Tag == rel)
                    {
                        string name = rel.Name;
                        if (rel.RpfFileEntry != null)
                        {
                            name = rel.RpfFileEntry.Name;
                        }
                        anode.Text = changestr + name;
                        break;
                    }
                }
            }
        }
        public void SetGrassBatchHasChanged(YmapGrassInstanceBatch batch, bool changed)
        {
            if (ProjectTreeView.Nodes.Count > 0)
            {
                var gbnode = FindGrassTreeNode(batch);
                if (gbnode == null) return;
                string changestr = changed ? "*" : "";
                if (gbnode.Tag == batch)
                {
                    string name = batch.ToString();
                    gbnode.Text = changestr + name;
                }
            }
        }









        private TreeNode GetChildTreeNode(TreeNode node, string name)
        {
            if (node == null) return null;
            var nodes = node.Nodes.Find(name, false);
            if ((nodes == null) || (nodes.Length != 1)) return null;
            return nodes[0];
        }
        public TreeNode FindYmapTreeNode(YmapFile ymap)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var ymapsnode = GetChildTreeNode(projnode, "Ymap");
            if (ymapsnode == null) return null;
            for (int i = 0; i < ymapsnode.Nodes.Count; i++)
            {
                var ymapnode = ymapsnode.Nodes[i];
                if (ymapnode.Tag == ymap) return ymapnode;
            }
            return null;
        }
        public TreeNode FindEntityTreeNode(YmapEntityDef ent)
        {
            if (ent == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(ent.Ymap);
            if (ymapnode == null) return null;
            var entsnode = GetChildTreeNode(ymapnode, "Entities");
            if (entsnode == null) return null;
            for (int i = 0; i < entsnode.Nodes.Count; i++)
            {
                TreeNode entnode = entsnode.Nodes[i];
                if (entnode.Tag == ent) return entnode;
            }
            return null;
        }
        public TreeNode FindCarGenTreeNode(YmapCarGen cargen)
        {
            if (cargen == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(cargen.Ymap);
            if (ymapnode == null) return null;
            var cargensnode = GetChildTreeNode(ymapnode, "CarGens");
            if (cargensnode == null) return null;
            for (int i = 0; i < cargensnode.Nodes.Count; i++)
            {
                TreeNode cargennode = cargensnode.Nodes[i];
                if (cargennode.Tag == cargen) return cargennode;
            }
            return null;
        }
        public TreeNode FindLodLightTreeNode(YmapLODLight lodlight)
        {
            if (lodlight == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(lodlight.Ymap);
            if (ymapnode == null) return null;
            var lodlightsnode = GetChildTreeNode(ymapnode, "LodLights");
            if (lodlightsnode == null) return null;
            for (int i = 0; i < lodlightsnode.Nodes.Count; i++)
            {
                TreeNode lodlightnode = lodlightsnode.Nodes[i];
                if (lodlightnode.Tag == lodlight) return lodlightnode;
            }
            return null;
        }
        public TreeNode FindBoxOccluderTreeNode(YmapBoxOccluder box)
        {
            if (box == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(box.Ymap);
            if (ymapnode == null) return null;
            var boxesnode = GetChildTreeNode(ymapnode, "BoxOccluders");
            if (boxesnode == null) return null;
            for (int i = 0; i < boxesnode.Nodes.Count; i++)
            {
                TreeNode boxnode = boxesnode.Nodes[i];
                if (boxnode.Tag == box) return boxnode;
            }
            return null;
        }
        public TreeNode FindOccludeModelTreeNode(YmapOccludeModel model)
        {
            if (model == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(model.Ymap);
            if (ymapnode == null) return null;
            var modelsnode = GetChildTreeNode(ymapnode, "OccludeModels");
            if (modelsnode == null) return null;
            for (int i = 0; i < modelsnode.Nodes.Count; i++)
            {
                TreeNode modelnode = modelsnode.Nodes[i];
                if (modelnode.Tag == model) return modelnode;
            }
            return null;
        }
        public TreeNode FindOccludeModelTriangleTreeNode(YmapOccludeModelTriangle tri)
        {
            if (tri == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(tri.Ymap);
            if (ymapnode == null) return null;
            var modelsnode = GetChildTreeNode(ymapnode, "OccludeModels");
            if (modelsnode == null) return null;
            for (int i = 0; i < modelsnode.Nodes.Count; i++)
            {
                TreeNode modelnode = modelsnode.Nodes[i];
                if (modelnode.Tag == tri.Model) return modelnode;
            }
            return null;
        }
        public TreeNode FindGrassTreeNode(YmapGrassInstanceBatch batch)
        {
            if (batch == null) return null;
            TreeNode ymapnode = FindYmapTreeNode(batch.Ymap);
            if (ymapnode == null) return null;
            var batchnode = GetChildTreeNode(ymapnode, "GrassBatches");
            if (batchnode == null) return null;
            for (int i = 0; i < batchnode.Nodes.Count; i++)
            {
                TreeNode grassnode = batchnode.Nodes[i];
                if (grassnode.Tag == batch) return grassnode;
            }
            return null;
        }
        public TreeNode FindYtypTreeNode(YtypFile ytyp)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var ytypsnode = GetChildTreeNode(projnode, "Ytyp");
            if (ytypsnode == null) return null;
            for (int i = 0; i < ytypsnode.Nodes.Count; i++)
            {
                var ytypnode = ytypsnode.Nodes[i];
                if (ytypnode.Tag == ytyp) return ytypnode;
            }
            return null;
        }
        public TreeNode FindArchetypeTreeNode(Archetype archetype)
        {
            if (archetype == null) return null;
            var ytypnode = FindYtypTreeNode(archetype.Ytyp);
            if (ytypnode == null) return null;
            var archetypenode = GetChildTreeNode(ytypnode, "Archetypes");
            if (archetypenode == null) return null;
            for (int i = 0; i < archetypenode.Nodes.Count; i++)
            {
                var archnode = archetypenode.Nodes[i];
                if (archnode.Tag == archetype) return archnode;
            }
            return null;
        }
        public TreeNode FindMloRoomTreeNode(MCMloRoomDef room)
        {
            if (room == null) return null;

            var mloarchetypenode = FindArchetypeTreeNode(room.OwnerMlo);
            if (mloarchetypenode != null)
            {
                var roomsnode = GetChildTreeNode(mloarchetypenode, "Rooms");
                if (roomsnode == null) return null;

                for (int j = 0; j < roomsnode.Nodes.Count; j++)
                {
                    var roomnode = roomsnode.Nodes[j];
                    if (roomnode.Tag == room) return roomnode;
                }
            }

            return null;
        }
        public TreeNode FindMloPortalTreeNode(MCMloPortalDef portal)
        {
            if (portal == null) return null;

            var mloarchetypenode = FindArchetypeTreeNode(portal.OwnerMlo);
            if (mloarchetypenode != null)
            {
                var portalsnode = GetChildTreeNode(mloarchetypenode, "Portals");
                if (portalsnode == null) return null;

                for (int j = 0; j < portalsnode.Nodes.Count; j++)
                {
                    var portalnode = portalsnode.Nodes[j];
                    if (portalnode.Tag == portal) return portalnode;
                }
            }

            return null;
        }
        public TreeNode FindMloEntitySetTreeNode(MCMloEntitySet entset)
        {
            if (entset == null) return null;

            var mloarchetypenode = FindArchetypeTreeNode(entset.OwnerMlo);
            if (mloarchetypenode != null)
            {
                var entsetsnode = GetChildTreeNode(mloarchetypenode, "EntitySets");
                if (entsetsnode == null) return null;

                for (int j = 0; j < entsetsnode.Nodes.Count; j++)
                {
                    var entsetnode = entsetsnode.Nodes[j];
                    if (entsetnode.Tag == entset) return entsetnode;
                }
            }

            return null;
        }
        public TreeNode FindMloEntityTreeNode(MCEntityDef ent)
        {
            var entityroom = ent?.OwnerMlo?.GetEntityRoom(ent);
            if (entityroom != null)
            {
                var roomnode = FindMloRoomTreeNode(entityroom);
                if (roomnode != null)
                {
                    for (var k = 0; k < roomnode.Nodes.Count; k++)
                    {
                        var entitynode = roomnode.Nodes[k];
                        if (entitynode.Tag == ent) return entitynode;
                    }
                }
            }

            var entityportal = ent?.OwnerMlo?.GetEntityPortal(ent);
            if (entityportal != null)
            {
                var portalnode = FindMloPortalTreeNode(entityportal);
                if (portalnode != null)
                {
                    for (var k = 0; k < portalnode.Nodes.Count; k++)
                    {
                        var entitynode = portalnode.Nodes[k];
                        if (entitynode.Tag == ent) return entitynode;
                    }
                }
            }

            var entityset = ent?.OwnerMlo?.GetEntitySet(ent);
            if (entityset != null)
            {
                var setnode = FindMloEntitySetTreeNode(entityset);
                if (setnode != null)
                {
                    for (var k = 0; k < setnode.Nodes.Count; k++)
                    {
                        var entitynode = setnode.Nodes[k];
                        if (entitynode.Tag == ent) return entitynode;
                    }
                }
            }

            return null;
        }
        public TreeNode FindYbnTreeNode(YbnFile ybn)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var ybnsnode = GetChildTreeNode(projnode, "Ybn");
            if (ybnsnode == null) return null;
            for (int i = 0; i < ybnsnode.Nodes.Count; i++)
            {
                var ybnnode = ybnsnode.Nodes[i];
                if (ybnnode.Tag == ybn) return ybnnode;
            }
            return null;
        }
        public TreeNode FindCollisionBoundsTreeNode(Bounds b)
        {
            if (b == null) return null;
            var bnode = (b.Parent != null) ? FindCollisionBoundsTreeNode(b.Parent) : FindYbnTreeNode(b.GetRootYbn());
            if (bnode == null) return null;
            for (int i = 0; i < bnode.Nodes.Count; i++)
            {
                var nnode = bnode.Nodes[i];
                if (nnode.Tag == b) return nnode;
            }
            return null;
        }
        public TreeNode FindCollisionPolyTreeNode(BoundPolygon p)
        {
            if (p == null) return null;
            var ybnnode = FindCollisionBoundsTreeNode(p.Owner);
            var polynode = GetChildTreeNode(ybnnode, "EditPoly");
            if (polynode == null) return null;
            polynode.Tag = p;
            return polynode;
        }
        public TreeNode FindCollisionVertexTreeNode(BoundVertex v)
        {
            if (v == null) return null;
            var ybnnode = FindCollisionBoundsTreeNode(v.Owner);
            var vertnode = GetChildTreeNode(ybnnode, "EditVertex");
            if (vertnode == null) return null;
            vertnode.Tag = v;
            return vertnode;
        }
        public TreeNode FindYndTreeNode(YndFile ynd)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var yndsnode = GetChildTreeNode(projnode, "Ynd");
            if (yndsnode == null) return null;
            for (int i = 0; i < yndsnode.Nodes.Count; i++)
            {
                var yndnode = yndsnode.Nodes[i];
                if (yndnode.Tag == ynd) return yndnode;
            }
            return null;
        }
        public TreeNode FindPathNodeTreeNode(YndNode n)
        {
            if (n == null) return null;
            TreeNode yndnode = FindYndTreeNode(n.Ynd);
            var nodesnode = GetChildTreeNode(yndnode, "Nodes");
            if (nodesnode == null) return null;
            for (int i = 0; i < nodesnode.Nodes.Count; i++)
            {
                TreeNode nnode = nodesnode.Nodes[i];
                if (nnode.Tag == n) return nnode;
            }
            return null;
        }
        public TreeNode FindYnvTreeNode(YnvFile ynv)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var ynvsnode = GetChildTreeNode(projnode, "Ynv");
            if (ynvsnode == null) return null;
            for (int i = 0; i < ynvsnode.Nodes.Count; i++)
            {
                var yndnode = ynvsnode.Nodes[i];
                if (yndnode.Tag == ynv) return yndnode;
            }
            return null;
        }
        public TreeNode FindNavPolyTreeNode(YnvPoly p)
        {
            if (p == null) return null;
            TreeNode ynvnode = FindYnvTreeNode(p.Ynv);
            var polynode = GetChildTreeNode(ynvnode, "EditPoly");
            if (polynode == null) return null;
            polynode.Tag = p;
            return polynode;
        }
        public TreeNode FindNavPointTreeNode(YnvPoint p)
        {
            if (p == null) return null;
            TreeNode ynvnode = FindYnvTreeNode(p.Ynv);
            var pointnode = GetChildTreeNode(ynvnode, "EditPoint");
            if (pointnode == null) return null;
            pointnode.Tag = p;
            return pointnode;
            //for (int i = 0; i < pointsnode.Nodes.Count; i++)
            //{
            //    TreeNode pnode = pointsnode.Nodes[i];
            //    if (pnode.Tag == p) return pnode;
            //}
            //return null;
        }
        public TreeNode FindNavPortalTreeNode(YnvPortal p)
        {
            if (p == null) return null;
            TreeNode ynvnode = FindYnvTreeNode(p.Ynv);
            var portalnode = GetChildTreeNode(ynvnode, "EditPortal");
            if (portalnode == null) return null;
            portalnode.Tag = p;
            return portalnode;
            //for (int i = 0; i < portalsnode.Nodes.Count; i++)
            //{
            //    TreeNode pnode = portalsnode.Nodes[i];
            //    if (pnode.Tag == p) return pnode;
            //}
            //return null;
        }
        public TreeNode FindTrainTrackTreeNode(TrainTrack track)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var trainsnode = GetChildTreeNode(projnode, "Trains");
            if (trainsnode == null) return null;
            for (int i = 0; i < trainsnode.Nodes.Count; i++)
            {
                var trainnode = trainsnode.Nodes[i];
                if (trainnode.Tag == track) return trainnode;
            }
            return null;
        }
        public TreeNode FindTrainNodeTreeNode(TrainTrackNode n)
        {
            if (n == null) return null;
            TreeNode tracknode = FindTrainTrackTreeNode(n.Track);
            var nodesnode = GetChildTreeNode(tracknode, "Nodes");
            if (nodesnode == null) return null;
            for (int i = 0; i < nodesnode.Nodes.Count; i++)
            {
                TreeNode nnode = nodesnode.Nodes[i];
                if (nnode.Tag == n) return nnode;
            }
            return null;
        }
        public TreeNode FindScenarioTreeNode(YmtFile ymt)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var scenariosnode = GetChildTreeNode(projnode, "Scenarios");
            if (scenariosnode == null) return null;
            for (int i = 0; i < scenariosnode.Nodes.Count; i++)
            {
                var ymtnode = scenariosnode.Nodes[i];
                if (ymtnode.Tag == ymt) return ymtnode;
            }
            return null;
        }
        public TreeNode FindScenarioNodeTreeNode(ScenarioNode p)
        {
            if (p == null) return null;
            TreeNode ymtnode = FindScenarioTreeNode(p.Ymt);
            var pointsnode = GetChildTreeNode(ymtnode, "Points");
            if (pointsnode == null) return null;
            for (int i = 0; i < pointsnode.Nodes.Count; i++)
            {
                TreeNode pnode = pointsnode.Nodes[i];
                if (pnode.Tag == p) return pnode;
            }
            return null;
        }
        public TreeNode FindAudioRelTreeNode(RelFile rel)
        {
            if (ProjectTreeView.Nodes.Count <= 0) return null;
            var projnode = ProjectTreeView.Nodes[0];
            var scenariosnode = GetChildTreeNode(projnode, "AudioRels");
            if (scenariosnode == null) return null;
            for (int i = 0; i < scenariosnode.Nodes.Count; i++)
            {
                var ymtnode = scenariosnode.Nodes[i];
                if (ymtnode.Tag == rel) return ymtnode;
            }
            return null;
        }
        public TreeNode FindAudioZoneTreeNode(AudioPlacement zone)
        {
            if (zone == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(zone.RelFile);
            var zonenode = GetChildTreeNode(relnode, "AmbientZones");
            if (zonenode == null) return null;
            //zonenode.Tag = zone;
            for (int i = 0; i < zonenode.Nodes.Count; i++)
            {
                TreeNode znode = zonenode.Nodes[i];
                if (znode.Tag == zone.AudioZone) return znode;
            }
            return zonenode;
        }
        public TreeNode FindAudioEmitterTreeNode(AudioPlacement emitter)
        {
            if (emitter == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(emitter.RelFile);
            var zonenode = GetChildTreeNode(relnode, "AmbientEmitters");
            if (zonenode == null) return null;
            //zonenode.Tag = emitter;
            for (int i = 0; i < zonenode.Nodes.Count; i++)
            {
                TreeNode znode = zonenode.Nodes[i];
                if (znode.Tag == emitter.AudioEmitter) return znode;
            }
            return zonenode;
        }
        public TreeNode FindAudioZoneListTreeNode(Dat151AmbientZoneList list)
        {
            if (list == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(list.Rel);
            var zonelistsnode = GetChildTreeNode(relnode, "AmbientZoneLists");
            if (zonelistsnode == null) return null;
            for (int i = 0; i < zonelistsnode.Nodes.Count; i++)
            {
                TreeNode lnode = zonelistsnode.Nodes[i];
                if (lnode.Tag == list) return lnode;
            }
            return null;
        }
        public TreeNode FindAudioEmitterListTreeNode(Dat151StaticEmitterList list)
        {
            if (list == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(list.Rel);
            var emitterlistsnode = GetChildTreeNode(relnode, "AmbientEmitterLists");
            if (emitterlistsnode == null) return null;
            for (int i = 0; i < emitterlistsnode.Nodes.Count; i++)
            {
                TreeNode enode = emitterlistsnode.Nodes[i];
                if (enode.Tag == list) return enode;
            }
            return null;
        }
        public TreeNode FindAudioInteriorTreeNode(Dat151Interior interior)
        {
            if (interior == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(interior.Rel);
            var interiorsnode = GetChildTreeNode(relnode, "Interiors");
            if (interiorsnode == null) return null;
            for (int i = 0; i < interiorsnode.Nodes.Count; i++)
            {
                TreeNode enode = interiorsnode.Nodes[i];
                if (enode.Tag == interior) return enode;
            }
            return null;
        }
        public TreeNode FindAudioInteriorRoomTreeNode(Dat151InteriorRoom room)
        {
            if (room == null) return null;
            TreeNode relnode = FindAudioRelTreeNode(room.Rel);
            var roomsnode = GetChildTreeNode(relnode, "InteriorRooms");
            if (roomsnode == null) return null;
            for (int i = 0; i < roomsnode.Nodes.Count; i++)
            {
                TreeNode enode = roomsnode.Nodes[i];
                if (enode.Tag == room) return enode;
            }
            return null;
        }





        public void DeselectNode()
        {
            ProjectTreeView.SelectedNode = null;
        }
        public void TrySelectYmapTreeNode(YmapFile ymap)
        {
            TreeNode ymapnode = FindYmapTreeNode(ymap);
            if (ymapnode != null)
            {
                if (ProjectTreeView.SelectedNode == ymapnode)
                {
                    OnItemSelected?.Invoke(ymap);
                }
                else
                {
                    ProjectTreeView.SelectedNode = ymapnode;
                }
            }
        }
        public void TrySelectEntityTreeNode(YmapEntityDef ent)
        {
            TreeNode entnode = FindEntityTreeNode(ent);
            if (entnode != null)
            {
                if (ProjectTreeView.SelectedNode == entnode)
                {
                    OnItemSelected?.Invoke(ent);
                }
                else
                {
                    ProjectTreeView.SelectedNode = entnode;
                }
            }
        }
        public void TrySelectCarGenTreeNode(YmapCarGen cargen)
        {
            TreeNode cargennode = FindCarGenTreeNode(cargen);
            if (cargennode != null)
            {
                if (ProjectTreeView.SelectedNode == cargennode)
                {
                    OnItemSelected?.Invoke(cargen);
                }
                else
                {
                    ProjectTreeView.SelectedNode = cargennode;
                }
            }
        }
        public void TrySelectLodLightTreeNode(YmapLODLight lodlight)
        {
            TreeNode lodlightnode = FindLodLightTreeNode(lodlight);
            if (lodlightnode != null)
            {
                if (ProjectTreeView.SelectedNode == lodlightnode)
                {
                    OnItemSelected?.Invoke(lodlight);
                }
                else
                {
                    ProjectTreeView.SelectedNode = lodlightnode;
                }
            }
        }
        public void TrySelectBoxOccluderTreeNode(YmapBoxOccluder box)
        {
            TreeNode boxnode = FindBoxOccluderTreeNode(box);
            if (boxnode != null)
            {
                if (ProjectTreeView.SelectedNode == boxnode)
                {
                    OnItemSelected?.Invoke(box);
                }
                else
                {
                    ProjectTreeView.SelectedNode = boxnode;
                }
            }
        }
        public void TrySelectOccludeModelTreeNode(YmapOccludeModel model)
        {
            TreeNode modelnode = FindOccludeModelTreeNode(model);
            if (modelnode != null)
            {
                if (ProjectTreeView.SelectedNode == modelnode)
                {
                    OnItemSelected?.Invoke(model);
                }
                else
                {
                    ProjectTreeView.SelectedNode = modelnode;
                }
            }
        }
        public void TrySelectOccludeModelTriangleTreeNode(YmapOccludeModelTriangle tri)
        {
            TreeNode trinode = FindOccludeModelTriangleTreeNode(tri);
            if (trinode != null)
            {
                if (ProjectTreeView.SelectedNode == trinode)
                {
                    OnItemSelected?.Invoke(tri);
                }
                else
                {
                    trinode.Tag = tri;//hack to allow the model's node to be selected instead
                    ProjectTreeView.SelectedNode = trinode;
                    trinode.Tag = tri.Model;
                }
            }
        }
        public void TrySelectGrassBatchTreeNode(YmapGrassInstanceBatch grassBatch)
        {
            TreeNode grassNode = FindGrassTreeNode(grassBatch);
            if (grassNode != null)
            {
                if (ProjectTreeView.SelectedNode == grassNode)
                {
                    OnItemSelected?.Invoke(grassNode);
                }
                else
                {
                    ProjectTreeView.SelectedNode = grassNode;
                }
            }
        }
        public void TrySelectMloEntityTreeNode(MCEntityDef ent)
        {
            TreeNode entnode = FindMloEntityTreeNode(ent);
            if (entnode != null)
            {
                if (ProjectTreeView.SelectedNode == entnode)
                {
                    OnItemSelected?.Invoke(ent);
                }
                else
                {
                    ProjectTreeView.SelectedNode = entnode;
                }
            }
        }
        public void TrySelectMloRoomTreeNode(MCMloRoomDef room)
        {
            TreeNode roomnode = FindMloRoomTreeNode(room);
            if (roomnode != null)
            {
                if (ProjectTreeView.SelectedNode == roomnode)
                {
                    OnItemSelected?.Invoke(room);
                }
                else
                {
                    ProjectTreeView.SelectedNode = roomnode;
                }
            }
        }
        public void TrySelectMloPortalTreeNode(MCMloPortalDef portal)
        {
            TreeNode portalnode = FindMloPortalTreeNode(portal);
            if (portalnode != null)
            {
                if (ProjectTreeView.SelectedNode == portalnode)
                {
                    OnItemSelected?.Invoke(portal);
                }
                else
                {
                    ProjectTreeView.SelectedNode = portalnode;
                }
            }
        }
        public void TrySelectMloEntitySetTreeNode(MCMloEntitySet set)
        {
            TreeNode setnode = FindMloEntitySetTreeNode(set);
            if (setnode != null)
            {
                if (ProjectTreeView.SelectedNode == setnode)
                {
                    OnItemSelected?.Invoke(set);
                }
                else
                {
                    ProjectTreeView.SelectedNode = setnode;
                }
            }
        }
        public void TrySelectArchetypeTreeNode(Archetype archetype)
        {
            TreeNode archetypenode = FindArchetypeTreeNode(archetype);
            if (archetypenode != null)
            {
                if (ProjectTreeView.SelectedNode == archetypenode)
                {
                    OnItemSelected?.Invoke(archetype);
                }
                else
                {
                    ProjectTreeView.SelectedNode = archetypenode;
                }
            }
        }
        public void TrySelectCollisionBoundsTreeNode(Bounds bounds)
        {
            TreeNode tnode = FindCollisionBoundsTreeNode(bounds);
            if (tnode == null)
            {
                tnode = FindYbnTreeNode(bounds?.GetRootYbn());
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(bounds);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectCollisionPolyTreeNode(BoundPolygon poly)
        {
            TreeNode tnode = FindCollisionPolyTreeNode(poly);
            if (tnode == null)
            {
                tnode = FindCollisionBoundsTreeNode(poly?.Owner);
            }
            if (tnode == null)
            {
                tnode = FindYbnTreeNode(poly?.Owner?.GetRootYbn());
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(poly);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectCollisionVertexTreeNode(BoundVertex vert)
        {
            TreeNode tnode = FindCollisionVertexTreeNode(vert);
            if (tnode == null)
            {
                tnode = FindCollisionBoundsTreeNode(vert?.Owner);
            }
            if (tnode == null)
            {
                tnode = FindYbnTreeNode(vert?.Owner?.GetRootYbn());
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(vert);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectPathNodeTreeNode(YndNode node)
        {
            TreeNode tnode = FindPathNodeTreeNode(node);
            if (tnode == null)
            {
                tnode = FindYndTreeNode(node?.Ynd);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(node);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectNavPolyTreeNode(YnvPoly poly)
        {
            TreeNode tnode = FindNavPolyTreeNode(poly);
            if (tnode == null)
            {
                tnode = FindYnvTreeNode(poly?.Ynv);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(poly);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectNavPointTreeNode(YnvPoint point)
        {
            TreeNode tnode = FindNavPointTreeNode(point);
            if (tnode == null)
            {
                tnode = FindYnvTreeNode(point?.Ynv);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(point);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectNavPortalTreeNode(YnvPortal portal)
        {
            TreeNode tnode = FindNavPortalTreeNode(portal);
            if (tnode == null)
            {
                tnode = FindYnvTreeNode(portal?.Ynv);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(portal);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectTrainNodeTreeNode(TrainTrackNode node)
        {
            TreeNode tnode = FindTrainNodeTreeNode(node);
            if (tnode == null)
            {
                tnode = FindTrainTrackTreeNode(node?.Track);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(node);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectScenarioTreeNode(YmtFile scenario)
        {
            TreeNode tnode = FindScenarioTreeNode(scenario);
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(scenario);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectScenarioNodeTreeNode(ScenarioNode node)
        {
            TreeNode tnode = FindScenarioNodeTreeNode(node);
            if (tnode == null)
            {
                tnode = FindScenarioTreeNode(node?.Ymt);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(node);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioRelTreeNode(RelFile rel)
        {
            TreeNode tnode = FindAudioRelTreeNode(rel);
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(rel);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioZoneTreeNode(AudioPlacement zone)
        {
            TreeNode tnode = FindAudioZoneTreeNode(zone);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(zone?.RelFile);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(zone);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioEmitterTreeNode(AudioPlacement emitter)
        {
            TreeNode tnode = FindAudioEmitterTreeNode(emitter);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(emitter?.RelFile);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(emitter);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioZoneListTreeNode(Dat151AmbientZoneList list)
        {
            TreeNode tnode = FindAudioZoneListTreeNode(list);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(list?.Rel);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(list);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioEmitterListTreeNode(Dat151StaticEmitterList list)
        {
            TreeNode tnode = FindAudioEmitterListTreeNode(list);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(list?.Rel);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(list);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioInteriorTreeNode(Dat151Interior interior)
        {
            TreeNode tnode = FindAudioInteriorTreeNode(interior);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(interior?.Rel);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(interior);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }
        public void TrySelectAudioInteriorRoomTreeNode(Dat151InteriorRoom room)
        {
            TreeNode tnode = FindAudioInteriorRoomTreeNode(room);
            if (tnode == null)
            {
                tnode = FindAudioRelTreeNode(room?.Rel);
            }
            if (tnode != null)
            {
                if (ProjectTreeView.SelectedNode == tnode)
                {
                    OnItemSelected?.Invoke(room);
                }
                else
                {
                    ProjectTreeView.SelectedNode = tnode;
                }
            }
        }




        public void UpdateYmapTreeNode(YmapFile ymap)
        {
            var tn = FindYmapTreeNode(ymap);
            if (tn != null)
            {
                tn.Text = ymap.RpfFileEntry?.Name ?? ymap.Name;
            }
        }
        public void UpdateYtypTreeNode(YtypFile ytyp)
        {
            var tn = FindYtypTreeNode(ytyp);
            if (tn != null)
            {
                tn.Text = ytyp.RpfFileEntry?.Name ?? ytyp.Name;
            }
        }
        public void UpdateYbnTreeNode(YbnFile ybn)
        {
            var tn = FindYbnTreeNode(ybn);
            if (tn != null)
            {
                tn.Text = ybn.RpfFileEntry?.Name ?? ybn.Name;
            }
        }
        public void UpdateYndTreeNode(YndFile ynd)
        {
            var tn = FindYndTreeNode(ynd);
            if (tn != null)
            {
                tn.Text = ynd.RpfFileEntry?.Name ?? ynd.Name;
            }
        }
        public void UpdateYnvTreeNode(YnvFile ynv)
        {
            var tn = FindYnvTreeNode(ynv);
            if (tn != null)
            {
                tn.Text = ynv.RpfFileEntry?.Name ?? ynv.Name;
            }
        }
        public void UpdateTrainTrackTreeNode(TrainTrack track)
        {
            var tn = FindTrainTrackTreeNode(track);
            if (tn != null)
            {
                tn.Text = track.RpfFileEntry?.Name ?? track.Name;
            }
        }
        public void UpdateScenarioTreeNode(YmtFile scenarios)
        {
            var tn = FindScenarioTreeNode(scenarios);
            if (tn != null)
            {
                tn.Text = scenarios.RpfFileEntry?.Name ?? scenarios.Name;
            }
        }
        public void UpdateAudioRelTreeNode(RelFile rel)
        {
            var tn = FindAudioRelTreeNode(rel);
            if (tn != null)
            {
                tn.Text = rel.RpfFileEntry?.Name ?? rel.Name;
            }
        }
        public void UpdateArchetypeTreeNode(Archetype archetype)
        {
            var tn = FindArchetypeTreeNode(archetype);
            if (tn != null)
            {
                tn.Text = archetype._BaseArchetypeDef.ToString();
            }
        }
        public void UpdateEntityTreeNode(YmapEntityDef ent)
        {
            if (ent == null) return;
            var tn = FindEntityTreeNode(ent);
            var name = ent.CEntityDef.archetypeName.ToString();
            if (tn != null)
            {
                if (ProjectForm.displayentityindexes)
                    tn.Text = $"[{tn.Index}] {name}";
                else
                    tn.Text = name;
            }
            else
            {
                var instance = ent.MloParent?.MloInstance;
                var mcent = instance?.TryGetArchetypeEntity(ent);
                tn = FindMloEntityTreeNode(mcent);
                if (tn != null)
                {
                    if (ProjectForm.displayentityindexes)
                        tn.Text = $"[{tn.Index}] {name}";
                    else
                        tn.Text = name;
                }
            }
        }
        public void UpdateCarGenTreeNode(YmapCarGen cargen)
        {
            var tn = FindCarGenTreeNode(cargen);
            if (tn != null)
            {
                tn.Text = cargen.ToString();
            }
        }
        public void UpdateLodLightTreeNode(YmapLODLight lodlight)
        {
            var tn = FindLodLightTreeNode(lodlight);
            if (tn != null)
            {
                tn.Text = lodlight.ToString();
            }
        }
        public void UpdateBoxOccluderTreeNode(YmapBoxOccluder box)
        {
            var tn = FindBoxOccluderTreeNode(box);
            if (tn != null)
            {
                tn.Text = box.ToString();
            }
        }
        public void UpdateOccludeModelTreeNode(YmapOccludeModel model)
        {
            var tn = FindOccludeModelTreeNode(model);
            if (tn != null)
            {
                tn.Text = model.ToString();
            }
        }
        public void UpdatePathNodeTreeNode(YndNode node)
        {
            var tn = FindPathNodeTreeNode(node);
            if (tn != null)
            {
                tn.Text = node._RawData.ToString();
            }
        }
        public void UpdateNavPolyTreeNode(YnvPoly poly)
        {
            var tn = FindNavPolyTreeNode(poly);
            if (tn != null)
            {
            }
        }
        public void UpdateTrainNodeTreeNode(TrainTrackNode node)
        {
            var tn = FindTrainNodeTreeNode(node);
            if (tn != null)
            {
                tn.Text = node.ToString();
            }
        }
        public void UpdateScenarioNodeTreeNode(ScenarioNode node)
        {
            var tn = FindScenarioNodeTreeNode(node);
            if (tn != null)
            {
                tn.Text = node.MedTypeName + ": " + node.StringText;
            }
        }
        public void UpdateAudioZoneTreeNode(AudioPlacement zone)
        {
            var tn = FindAudioZoneTreeNode(zone);
            if (tn != null)
            {
                tn.Text = zone.NameHash.ToString();
            }
        }
        public void UpdateAudioEmitterTreeNode(AudioPlacement emitter)
        {
            var tn = FindAudioEmitterTreeNode(emitter);
            if (tn != null)
            {
                tn.Text = emitter.NameHash.ToString();
            }
        }
        public void UpdateAudioZoneListTreeNode(Dat151AmbientZoneList list)
        {
            var tn = FindAudioZoneListTreeNode(list);
            if (tn != null)
            {
                tn.Text = list.NameHash.ToString();
            }
        }
        public void UpdateAudioEmitterListTreeNode(Dat151StaticEmitterList list)
        {
            var tn = FindAudioEmitterListTreeNode(list);
            if (tn != null)
            {
                tn.Text = list.NameHash.ToString();
            }
        }
        public void UpdateAudioInteriorTreeNode(Dat151Interior interior)
        {
            var tn = FindAudioInteriorTreeNode(interior);
            if (tn != null)
            {
                tn.Text = interior.NameHash.ToString();
            }
        }
        public void UpdateAudioInteriorRoomTreeNode(Dat151InteriorRoom room)
        {
            var tn = FindAudioInteriorRoomTreeNode(room);
            if (tn != null)
            {
                tn.Text = room.NameHash.ToString();
            }
        }



        public void RemoveEntityTreeNode(YmapEntityDef ent)
        {
            var tn = FindEntityTreeNode(ent);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "实体 (" + ent.Ymap.AllEntities.Length.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveCarGenTreeNode(YmapCarGen cargen)
        {
            var tn = FindCarGenTreeNode(cargen);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "车辆生成器 (" + cargen.Ymap.CarGenerators.Length.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveLodLightTreeNode(YmapLODLight lodlight)
        {
            var lodlights = lodlight?.LodLights?.LodLights;
            var tn = FindLodLightTreeNode(lodlight);
            if ((tn != null) && (tn.Parent != null) && (lodlights != null))
            {
                var pn = tn.Parent;
                var yn = pn.Parent;
                yn.Nodes.Remove(pn);
                pn = yn.Nodes.Add("远景灯光 (" + (lodlights?.Length.ToString() ?? "0") + ")");
                pn.Name = "LodLights";
                pn.Tag = lodlight.LodLights.Ymap;
                foreach (var ll in lodlights)
                {
                    var ntn = pn.Nodes.Add(ll.ToString());
                    ntn.Tag = ll;
                }
            }
        }
        public void RemoveBoxOccluderTreeNode(YmapBoxOccluder box)
        {
            var ymap = box?.Ymap;
            var tn = FindBoxOccluderTreeNode(box);
            if ((tn != null) && (tn.Parent != null) && (box != null))
            {
                var pn = tn.Parent;
                var yn = pn.Parent;
                yn.Nodes.Remove(pn);
                pn = yn.Nodes.Add("遮挡盒 (" + (ymap?.BoxOccluders?.Length.ToString() ?? "0") + ")");
                pn.Name = "BoxOccluders";
                pn.Tag = ymap;
                if (ymap.BoxOccluders != null)
                {
                    foreach (var b in ymap.BoxOccluders)
                    {
                        var ntn = pn.Nodes.Add(b.ToString());
                        ntn.Tag = b;
                    }
                }
            }
        }
        public void RemoveOccludeModelTreeNode(YmapOccludeModel model)
        {
            var ymap = model?.Ymap;
            var tn = FindOccludeModelTreeNode(model);
            if ((tn != null) && (tn.Parent != null) && (model != null))
            {
                var pn = tn.Parent;
                var yn = pn.Parent;
                yn.Nodes.Remove(pn);
                pn = yn.Nodes.Add("遮挡模型 (" + (ymap?.OccludeModels?.Length.ToString() ?? "0") + ")");
                pn.Name = "OccludeModels";
                pn.Tag = ymap;
                if (ymap.OccludeModels != null)
                {
                    foreach (var m in ymap.OccludeModels)
                    {
                        var ntn = pn.Nodes.Add(m.ToString());
                        ntn.Tag = m;
                    }
                }
            }
        }
        public void RemoveGrassBatchTreeNode(YmapGrassInstanceBatch batch)
        {
            var tn = FindGrassTreeNode(batch);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "批处理实例草 (" + batch.Ymap.GrassInstanceBatches.Length.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveArchetypeTreeNode(Archetype archetype)
        {
            var tn = FindArchetypeTreeNode(archetype);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "定义类型 (" + archetype.Ytyp.AllArchetypes.Length.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveMloEntityTreeNode(MCEntityDef ent)
        {
            var tn = FindMloEntityTreeNode(ent);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveMloRoomTreeNode(MCMloRoomDef room)
        {
            var tn = FindMloRoomTreeNode(room);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "房间 (" + (room.OwnerMlo?.rooms?.Length.ToString() ?? "0") + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveMloPortalTreeNode(MCMloPortalDef portal)
        {
            var tn = FindMloPortalTreeNode(portal);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "门户 (" + (portal.OwnerMlo?.portals?.Length.ToString() ?? "0") + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveMloEntitySetTreeNode(MCMloEntitySet set)
        {
            var tn = FindMloEntitySetTreeNode(set);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "实体预设 (" + (set.OwnerMlo?.entitySets?.Length.ToString() ?? "0") + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveCollisionBoundsTreeNode(Bounds bounds)
        {
            var tn = FindCollisionBoundsTreeNode(bounds);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemovePathNodeTreeNode(YndNode node)
        {
            var tn = FindPathNodeTreeNode(node);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "节点 (" + node.Ynd.Nodes.Length.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveTrainNodeTreeNode(TrainTrackNode node)
        {
            var tn = FindTrainNodeTreeNode(node);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "节点 (" + node.Track.Nodes.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveScenarioNodeTreeNode(ScenarioNode node)
        {
            var tn = FindScenarioNodeTreeNode(node);
            if ((tn != null) && (tn.Parent != null))
            {
                tn.Parent.Text = "点 (" + (node.Ymt?.ScenarioRegion?.Nodes?.Count ?? 0).ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioZoneTreeNode(AudioPlacement zone)
        {
            var tn = FindAudioZoneTreeNode(zone);
            if ((tn != null) && (tn.Parent != null))
            {
                var zones = new List<Dat151AmbientZone>();
                foreach (var reldata in zone.RelFile.RelDatas)
                {
                    if (reldata is Dat151AmbientZone)
                    {
                        zones.Add(reldata as Dat151AmbientZone);
                    }
                }

                tn.Parent.Text = "环境音区域 (" + zones.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioEmitterTreeNode(AudioPlacement emitter)
        {
            var tn = FindAudioEmitterTreeNode(emitter);
            if ((tn != null) && (tn.Parent != null))
            {
                var emitters = new List<Dat151AmbientRule>();
                foreach (var reldata in emitter.RelFile.RelDatas)
                {
                    if (reldata is Dat151AmbientRule)
                    {
                        emitters.Add(reldata as Dat151AmbientRule);
                    }
                }

                tn.Parent.Text = "环境音发生器 (" + emitters.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioZoneListTreeNode(Dat151AmbientZoneList list)
        {
            var tn = FindAudioZoneListTreeNode(list);
            if ((tn != null) && (tn.Parent != null))
            {
                var zonelists = new List<Dat151AmbientZoneList>();
                foreach (var reldata in list.Rel.RelDatas)
                {
                    if (reldata is Dat151AmbientZoneList)
                    {
                        zonelists.Add(reldata as Dat151AmbientZoneList);
                    }
                }

                tn.Parent.Text = "环境音区域列表 (" + zonelists.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioEmitterListTreeNode(Dat151StaticEmitterList list)
        {
            var tn = FindAudioEmitterListTreeNode(list);
            if ((tn != null) && (tn.Parent != null))
            {
                var emitterlists = new List<Dat151StaticEmitterList>();
                foreach (var reldata in list.Rel.RelDatas)
                {
                    if (reldata is Dat151StaticEmitterList)
                    {
                        emitterlists.Add(reldata as Dat151StaticEmitterList);
                    }
                }

                tn.Parent.Text = "环境音发生器列表 (" + emitterlists.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioInteriorTreeNode(Dat151Interior interior)
        {
            var tn = FindAudioInteriorTreeNode(interior);
            if ((tn != null) && (tn.Parent != null))
            {
                var interiors = new List<Dat151Interior>();
                foreach (var reldata in interior.Rel.RelDatas)
                {
                    if (reldata is Dat151Interior)
                    {
                        interiors.Add(reldata as Dat151Interior);
                    }
                }

                tn.Parent.Text = "室内 (" + interiors.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }
        public void RemoveAudioInteriorRoomTreeNode(Dat151InteriorRoom room)
        {
            var tn = FindAudioInteriorRoomTreeNode(room);
            if ((tn != null) && (tn.Parent != null))
            {
                var interiors = new List<Dat151InteriorRoom>();
                foreach (var reldata in room.Rel.RelDatas)
                {
                    if (reldata is Dat151InteriorRoom)
                    {
                        interiors.Add(reldata as Dat151InteriorRoom);
                    }
                }

                tn.Parent.Text = "室内房间 (" + interiors.Count.ToString() + ")";
                tn.Parent.Nodes.Remove(tn);
            }
        }





        public event ProjectExplorerItemSelectHandler OnItemSelected;
        public event ProjectExplorerItemActivateHandler OnItemActivated;


        private void ClearSelectedNodes()
        {
            foreach (var node in SelectedNodes)
            {
                node.BackColor = Color.Empty;
                node.ForeColor = Color.Empty;
            }
            SelectedNodes.Clear();
        }


        private void ProjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool focused = ProjectForm?.ContainsFocus ?? this.ContainsFocus;
            bool addSelection = focused && ((ModifierKeys & Keys.Control) > 0);
            bool fillSelection = focused && ((ModifierKeys & Keys.Shift) > 0);
            if (addSelection)
            {
                if (SelectedNodes.Contains(e.Node))
                {
                    e.Node.BackColor = Color.Empty;
                    e.Node.ForeColor = Color.Empty;
                    SelectedNodes.Remove(e.Node);
                }
                else
                {
                    SelectedNodes.Add(e.Node);
                }
            }
            else if (fillSelection)
            {
                var snode = (SelectedNodes.Count == 0) ? null : SelectedNodes[SelectedNodes.Count - 1];
                var pnode = snode?.Parent;

                if ((pnode == null) || (pnode != e.Node?.Parent))
                {
                    SelectedNodes.Add(e.Node);
                }
                else
                {
                    bool start = false;
                    ClearSelectedNodes();
                    foreach (TreeNode cnode in pnode.Nodes)
                    {
                        if (start)
                        {
                            SelectedNodes.Add(cnode);
                        }
                        if (cnode == snode)
                        {
                            if (start) break;
                            else start = true;
                            SelectedNodes.Add(cnode);
                        }
                        if (cnode == e.Node)
                        {
                            if (start) break;
                            else start = true;
                            SelectedNodes.Add(cnode);
                        }
                    }
                }
            }
            else
            {
                ClearSelectedNodes();
                SelectedNodes.Add(e.Node);
            }
            if (SelectedNodes.Count > 1)
            {
                var objs = new List<object>();
                foreach (var node in SelectedNodes)
                {
                    node.BackColor = SystemColors.Highlight;
                    node.ForeColor = SystemColors.HighlightText;
                    objs.Add(node.Tag);
                }
                OnItemSelected?.Invoke(objs.ToArray());
            }
            else
            {
                OnItemSelected?.Invoke(ProjectTreeView.SelectedNode?.Tag);
            }
        }
        private void ProjectTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (ProjectTreeView.SelectedNode != null)
            {
                OnItemActivated?.Invoke(ProjectTreeView.SelectedNode.Tag);
            }
        }

        private void ProjectTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //if (e.Node.Tag != CurrentProjectFile) return; //disabling doubleclick expand/collapse only for project node
            if (inDoubleClick == true && e.Action == TreeViewAction.Collapse) e.Cancel = true;
        }
        private void ProjectTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //if (e.Node.Tag != CurrentProjectFile) return; //disabling doubleclick expand/collapse only for project node
            if (inDoubleClick == true && e.Action == TreeViewAction.Expand) e.Cancel = true;
        }
        private void ProjectTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            inDoubleClick = (e.Clicks > 1); //disabling doubleclick expand/collapse
        }

        private void ProjectTreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null) //disabling drag and drop text
                e.Effect = DragDropEffects.All;
        }

        private void ProjectTreeView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            ProjectForm.OpenFiles(files);

        }

    }
    public delegate void ProjectExplorerItemSelectHandler(object item);
    public delegate void ProjectExplorerItemActivateHandler(object item);
}
