using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Password_Manager
{
    internal class Usuario
    {
        private string masterKEY = "escribe tu clave aca!";
        public string path { get; set; }
        private string userPassword { get; set; }
        
        private string key_items { get; set; }

        public bool userCreate(string pas, string path)
        {
            seguridad encode = new seguridad();

            try
            {
                userPassword = encode.ComputeHash(pas);
            }
            catch (Exception)
            {
                return false;
            }

            List<key_item> keyList = new List<key_item>();
            StringKey_item(keyList, pas);

            return FileSave(path);
        }
        
        public bool userLogin(string pas, string path)
        {
            seguridad encode = new seguridad();

            loadProperties(path);

            if (encode.ComputeHash(pas) == userPassword)
            {
                return true;
            }
            userPassword = "";
            key_items = "";
            return false;
        }

        private void loadProperties(string path)
        {
            userSave read = new userSave();
            seguridad encode = new seguridad();

            string tmp = "";

            try
            {
                tmp = File.ReadAllText(path);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo leer", "error");
                return;
            }

            tmp = encode.desencriptar(tmp, masterKEY);
            
            try
            {
                read = JsonConvert.DeserializeObject<userSave>(tmp);
            }
            catch (Exception)
            {
                MessageBox.Show("archivo incorrecto", "error");
                throw;
            }

            userPassword = read.userPassword;
            key_items = read.key_Items;
        }

        public bool FileSave(string path)
        {
            string tmp = "";
            userSave userTmp = new userSave();
            seguridad encode = new seguridad();

            if (File.Exists(path))
            {
                try
                {
                    tmp = File.ReadAllText(path);
                }
                catch (Exception)
                {
                    throw;
                }


                tmp = encode.desencriptar(tmp, masterKEY);

                try
                {
                    userTmp = JsonConvert.DeserializeObject<userSave>(tmp);
                }
                catch (Exception)
                {

                    throw;
                }

                if (userTmp.userPassword != userPassword)
                {
                    MessageBox.Show("Archivo erroneo o dañado (no coincide)", "error");
                    return false;
                }
            }

            userTmp = new userSave();


            userTmp.userPassword = userPassword;
            userTmp.key_Items = key_items;

            tmp = JsonConvert.SerializeObject(userTmp);

            userTmp = new userSave();

            tmp = encode.encriptar(tmp, masterKEY);

            try
            {
                File.WriteAllText(path, tmp);
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo reescribir el archivo","error");
                return false;
                throw;
            }

            return true;
        }

        public List<key_item> getKeyList(string pas)
        {
            List<key_item> keyList = new List<key_item>();
            seguridad encode = new seguridad();
            string tmp = "";

            try
            {
                tmp = encode.desencriptar(key_items, pas);
            }
            catch (Exception)
            {
                throw;
            }
            
            try
            {
                keyList = JsonConvert.DeserializeObject<List<key_item>>(tmp);
            }
            catch (Exception)
            {
                MessageBox.Show("Imposible deserealizar \"key_items\"","Error");
                throw;
            }

            return keyList;
        }

        private void StringKey_item(List<key_item> tmp, string pas)
        {
            seguridad encode = new seguridad();
            
            key_items = JsonConvert.SerializeObject(tmp);

            key_items = encode.encriptar(key_items, pas);
        }

        public bool addKeyItem(key_item item, string pas)
        {
            List<key_item> tmp = getKeyList(pas);

            tmp.Add(item);
            StringKey_item(tmp,pas);
            
            return FileSave(path);
        }

        public bool removeKeyItem(key_item item, string pas)
        {
            List<key_item> tmp = getKeyList(pas);

            if (!tmp.Contains(item))
            {
                MessageBox.Show("No existe el item","Error");
                return false;
            }

            tmp.Remove(item);
            StringKey_item(tmp, pas);

            return FileSave(path);
        }

        public bool removeKeyItemAt(int index, string pas)
        {
            List<key_item> tmp = getKeyList(pas);

            if (MessageBox.Show("Seguro que desea borrar (" + tmp[index].Service + " - " + tmp[index].optional + ")","Advertencia!",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                tmp.RemoveAt(index);

                StringKey_item(tmp,pas);

                return FileSave(path);
            }

            return false;
        }

        public string getKey(string service, string description, string pas)
        {
            string key = "";

            List<key_item> tmp = getKeyList(pas);

            foreach (var item in tmp)
            {
                if (item.Service == service && item.optional == description)
                {
                    key = item.Key;
                    return key;
                }
            }

            return key;
        }
    }
}
