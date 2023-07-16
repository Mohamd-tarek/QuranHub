class TrieNode{

   public count: number;
   public character: string;
   public children: Map<string, TrieNode>;

   constructor(key: string) {
      this.count = 0;
      this.character = key;
      this.children = new Map<string, TrieNode>();
   }

}

export class Trie{

   root : TrieNode;
   constructor(){
      this.root = new TrieNode("");
   }

  public insert(key: string, _count: number = 1): void {

     let node = this.root;

      for (const char of key) {

        if (node.children.get(char) === undefined) {

              node.children.set(char, new TrieNode(char));
          }

          node = node.children.get(char) as TrieNode;
      }
      node.count += _count;
  }

  public search(key: string): boolean {

    let node = this.root;

    for (const char of key) {

        if(!node.children.get(char)) {
            return false;
        }

        node = node.children.get(char) as TrieNode;
    }

    return node.count > 0;
 }
 
   public findAllWords(node: TrieNode, output:any [], word : string = ""): void {
     word += node.character;

     if (node.count > 0) {

          let cur : any[] = [];
          cur.push(word);
          cur.push(node.count);
          output.push(cur);
      }
    
     for (const key of node.children.keys()) {

          this.findAllWords( node.children.get(key) as TrieNode, output, word);
      }
   }

   public find(key: string): any[]{
      let node = this.root;

      let output: [] = [];

     for (const char of key) {

       if (!node.children.get(char)) {

              return output;
       }

          node = node.children.get(char) as TrieNode;
      }

     this.findAllWords(node, output, key.substring(0, key.length - 1));

      return output;
   }

}
