using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Day2eEditor
{
    public partial class UniversalAttachmentPreview : Form
    {
        private FormController controller;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TitleLable
        {
            set
            {
                TitleLabel.Text = value;
            }
        }
        private bool _updatingChecks = false;

        public UniversalAttachmentPreview()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                null,
                CloseButton,
                null
            );
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpawnableType SpawnableType
        {
            get;
            set;
        }
        private void UniversalAttachmentPreview_Load(object sender, EventArgs e)
        {
            TreeNode IN = new TreeNode(SpawnableType.name)
            {
                Tag = SpawnableType
            };
            foreach (var item in SpawnableType.Items)
            {
                IN.Nodes.Add(CrteateSpawnableTypeNodes(item));
            }
            treeView1.Nodes.Add(IN);
            CheckAllNodes(treeView1.Nodes, true);
        }
        private void CheckAllNodes(TreeNodeCollection nodes, bool checkState)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = checkState;
                CheckAllNodes(node.Nodes, checkState);
            }
        }
        private TreeNode CrteateSpawnableTypeNodes(object item)
        {
            if (item is spawnableTypesHoarder)
            {
                return new TreeNode("hoarder")
                {
                    Tag = item as spawnableTypesHoarder
                };
            }
            else if (item is spawnableTypeTag)
            {
                return new TreeNode(getTagString(item as spawnableTypeTag))
                {
                    Tag = item as spawnableTypeTag
                };
            }
            else if (item is spawnableTypeDamage)
            {
                spawnableTypeDamage damage = item as spawnableTypeDamage;
                return CreateDamageNode(damage);
            }
            else if (item is spawnableTypeCargo)
            {
                spawnableTypeCargo cargo = item as spawnableTypeCargo;
                return createCargoNopdes(cargo);
            }
            else if (item is spawnableTypeAttachment)
            {
                spawnableTypeAttachment attachment = item as spawnableTypeAttachment;
                return createattachmentnodes(attachment);
            }
            return null;
        }
        private string getTagString(spawnableTypeTag spawnableTypeTag)
        {
            return $"tag : {spawnableTypeTag.name}";
        }
        private TreeNode createattachmentnodes(spawnableTypeAttachment attachment)
        {
            TreeNode attachmentnode = new TreeNode(getAttachmentString(attachment))
            {
                Tag = attachment
            };
            if (attachment.damage != null)
            {
                attachmentnode.Nodes.Add(CreateDamageNode(attachment.damage));
            }
            if (attachment.item.Count > 0)
            {
                foreach (spawnableTypeItem STI in attachment.item)
                {
                    attachmentnode.Nodes.Add(CreateItemNode(STI));
                }
            }
            return attachmentnode;
        }
        private string getAttachmentString(spawnableTypeAttachment attachment)
        {
            string attachmentstring = "Attachments :";
            if (attachment.preset != null)
            {
                attachmentstring += " Preset = " + attachment.preset;
            }
            if (attachment.chanceSpecified)
            {
                attachmentstring += " Chance = " + attachment.chance;
            }

            return attachmentstring;
        }
        private TreeNode createCargoNopdes(spawnableTypeCargo cargo)
        {

            TreeNode cargonode = new TreeNode(Getcargostring(cargo))
            {
                Tag = cargo
            };
            if (cargo.damage != null)
            {
                cargonode.Nodes.Add(CreateDamageNode(cargo.damage));
            }
            if (cargo.item.Count > 0)
            {
                foreach (spawnableTypeItem STI in cargo.item)
                {
                    cargonode.Nodes.Add(CreateItemNode(STI));
                }
            }
            return cargonode;
        }
        private string Getcargostring(spawnableTypeCargo cargo)
        {
            string cargostring = "Cargo :";
            if (cargo.preset != null)
            {
                cargostring += " Preset = " + cargo.preset;
            }
            if (cargo.chanceSpecified)
            {
                cargostring += " Chance = " + cargo.chance;
            }
            return cargostring;
        }
        private TreeNode CreateDamageNode(spawnableTypeDamage damage)
        {
            TreeNode damagenode = new TreeNode(GetDamageString(damage))
            {
                Tag = damage
            };
            return damagenode;
        }
        private string GetDamageString(spawnableTypeDamage damage)
        {
            return $"damage : quantmin={damage.min} quamtmax={damage.max}";
        }
        private TreeNode CreateItemNode(spawnableTypeItem sTI)
        {
            TreeNode treeNode = new TreeNode(GetItemString(sTI))
            {
                Tag = sTI
            };
            if (sTI.damage != null)
            {
                treeNode.Nodes.Add(CreateDamageNode(sTI.damage));
            }
            if (sTI.attachments.Count > 0)
            {
                foreach (spawnableTypeAttachment attachment in sTI.attachments)
                {
                    treeNode.Nodes.Add(createattachmentnodes(attachment));
                }
            }
            if (sTI.cargo.Count > 0)
            {
                foreach (spawnableTypeCargo cargo in sTI.cargo)
                {
                    treeNode.Nodes.Add(createCargoNopdes(cargo));
                }
            }

            return treeNode;
        }
        private string GetItemString(spawnableTypeItem sTI)
        {
            string itemstring = $"Item = {sTI.name}";
            if (sTI.equipSpecified)
            {
                itemstring += " equip = " + sTI.equip;
            }
            if (sTI.chanceSpecified)
            {
                itemstring += " Chance = " + sTI.chance;
            }
            if (sTI.quantminSpecified && sTI.quantmaxSpecified)
            {
                itemstring += " quantMin = " + sTI.quantmin + " quantMax = " + sTI.quantmax;
            }

            return itemstring;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_updatingChecks)
                return;

            _updatingChecks = true;

            try
            {
                // Update children
                SetChildCheckedState(e.Node, e.Node.Checked);

                // If checked, ensure parents are checked
                if (e.Node.Checked)
                {
                    CheckParents(e.Node.Parent);
                }
            }
            finally
            {
                _updatingChecks = false;
            }
        }
        private void SetChildCheckedState(TreeNode node, bool checkedState)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = checkedState;
                SetChildCheckedState(child, checkedState);
            }
        }
        private void CheckParents(TreeNode parent)
        {
            while (parent != null)
            {
                parent.Checked = true;
                parent = parent.Parent;
            }
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count == 0)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            TreeNode root = treeView1.Nodes[0];

            RemoveUncheckedRootItems(root);

            ProcessNode(root);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void RemoveUncheckedRootItems(TreeNode root)
        {
            for (int i = SpawnableType.Items.Count - 1; i >= 0; i--)
            {
                object item = SpawnableType.Items[i];

                bool foundCheckedNode = false;

                foreach (TreeNode node in root.Nodes)
                {
                    if (ReferenceEquals(node.Tag, item))
                    {
                        foundCheckedNode = node.Checked;
                        break;
                    }
                }

                if (!foundCheckedNode)
                {
                    SpawnableType.Items.RemoveAt(i);
                }
            }
        }

        private void ProcessNode(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                ProcessNode(child);
            }

            switch (node.Tag)
            {
                case spawnableTypeItem item:
                    RemoveUncheckedChildren(item, node);
                    break;

                case spawnableTypeAttachment attachment:
                    RemoveUncheckedChildren(attachment, node);
                    break;

                case spawnableTypeCargo cargo:
                    RemoveUncheckedChildren(cargo, node);
                    break;
            }
        }

        private void RemoveUncheckedChildren(
            spawnableTypeItem item,
            TreeNode node)
        {
            // Damage
            if (item.damage != null)
            {
                bool damageChecked = false;

                foreach (TreeNode child in node.Nodes)
                {
                    if (ReferenceEquals(child.Tag, item.damage))
                    {
                        damageChecked = child.Checked;
                        break;
                    }
                }

                if (!damageChecked)
                {
                    item.damage = null;
                }
            }

            // Attachments
            if (item.attachments != null)
            {
                RemoveUnchecked(item.attachments, node);
            }

            // Cargo
            if (item.cargo != null)
            {
                RemoveUnchecked(item.cargo, node);
            }
        }

        private void RemoveUncheckedChildren(
            spawnableTypeAttachment attachment,
            TreeNode node)
        {
            // Damage
            if (attachment.damage != null)
            {
                bool damageChecked = false;

                foreach (TreeNode child in node.Nodes)
                {
                    if (ReferenceEquals(child.Tag, attachment.damage))
                    {
                        damageChecked = child.Checked;
                        break;
                    }
                }

                if (!damageChecked)
                {
                    attachment.damage = null;
                }
            }

            // Child items
            if (attachment.item != null)
            {
                RemoveUnchecked(attachment.item, node);
            }
        }

        private void RemoveUncheckedChildren(
            spawnableTypeCargo cargo,
            TreeNode node)
        {
            // Damage
            if (cargo.damage != null)
            {
                bool damageChecked = false;

                foreach (TreeNode child in node.Nodes)
                {
                    if (ReferenceEquals(child.Tag, cargo.damage))
                    {
                        damageChecked = child.Checked;
                        break;
                    }
                }

                if (!damageChecked)
                {
                    cargo.damage = null;
                }
            }

            // Child items
            if (cargo.item != null)
            {
                RemoveUnchecked(cargo.item, node);
            }
        }

        private void RemoveUnchecked<T>(
            BindingList<T> collection,
            TreeNode parentNode)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (!IsChecked(parentNode, collection[i]))
                {
                    collection.RemoveAt(i);
                }
            }
        }

        private bool IsChecked(
            TreeNode parentNode,
            object target)
        {
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (ReferenceEquals(child.Tag, target))
                {
                    return child.Checked;
                }
            }

            return false;
        }
    }
}
